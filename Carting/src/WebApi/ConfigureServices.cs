using System.Reflection;
using Carting.WebApi.Application.Common.Behaviours;
using Carting.WebApi.Application.Common.Configuration;
using Carting.WebApi.Application.Common.Interfaces;
using Carting.WebApi.Filters;
using Carting.WebApi.Helpers;
using Carting.WebApi.Infrastructure.Consumers;
using Carting.WebApi.Infrastructure.Persistence;
using Carting.WebApi.Infrastructure.Services;
using Carting.WebApi.Middlewares;
using Carting.WebApi.Swagger;
using FluentValidation;
using FluentValidation.AspNetCore;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{
    public static IServiceCollection AddWebApiServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpContextAccessor();

        services.AddHealthChecks();
        services.AddTransient<IdentityAccessTokenLoggerMiddleware>();

        services.AddControllersWithViews(options =>
            options.Filters.Add<ApiExceptionFilterAttribute>())
                .AddFluentValidation(x => x.AutomaticValidationEnabled = false);

        // Customise default API behaviour
        services.Configure<ApiBehaviorOptions>(options =>
            options.SuppressModelStateInvalidFilter = true);

        services.AddMvc();
        services.AddControllers(options =>
        {
            options.Conventions.Add(new GroupingByNamespaceConvention());
        });

        services.AddApiVersioning(config => {
            config.DefaultApiVersion = new ApiVersion(1, 0);
            config.ReportApiVersions = true;
            config.AssumeDefaultVersionWhenUnspecified = true;
        });

        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "Carting API V1",
                Description = "API for Carting-related operations"
            });

            options.SwaggerDoc("v2", new OpenApiInfo
            {
                Version = "v2",
                Title = "Carting API V2",
                Description = "API for Carting-related operations"
            });

            var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename), true);

            options.OperationFilter<SwaggerParameterFilters>();
            options.DocumentFilter<SwaggerVersionMapping>();

            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
            });
            options.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                }
            },
                    new string[] {}
                }
    });
        });

        services.AddCustomAuthentication(configuration);
        return services;
    }

    public static IServiceCollection AddCustomAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        var identityUrl = configuration.GetSection("Identity").GetValue<string>("Url");

        // Accepts any access token issued by identity server
        services.AddAuthentication("Bearer")
            .AddJwtBearer("Bearer", options =>
            {
                options.Authority = identityUrl;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false
                };
            });

        // Adds an authorization policy to make sure the token is for scope 'catalog'
        services.AddAuthorization(options =>
        {
            options.AddPolicy("CartingApiScope", policy =>
            {
                policy.RequireAuthenticatedUser();
                policy.RequireClaim("scope", "carting");
            });
        });

        return services;
    }

    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ICartingDbContext, CartingDbContext>();
        services.AddTransient<IDateTime, DateTimeService>();

        var massTransitConfiguration = configuration.GetSection(MassTransitConfiguration.MassTransitConfigurationKey).Get<MassTransitConfiguration>();

        services.AddMassTransit(x =>
        {
            x.AddConsumer<ProductPriceChangedIntegrationEventConsumer>(typeof(ProductPriceChangedIntegrationEventConsumerDefinition));
            x.AddConsumer<ProductDeletedIntegrationEventConsumer>(typeof(ProductDeletedIntegrationEventConsumerDefinition));

            x.SetKebabCaseEndpointNameFormatter();

            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(massTransitConfiguration.Host, massTransitConfiguration.VirtualHost, h => {
                    h.Username(massTransitConfiguration.Username);
                    h.Password(massTransitConfiguration.Password);
                });

                cfg.ConfigureEndpoints(context);
            });
        });

        return services;
    }

    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>));
        return services;
    }

    public static IServiceCollection AddConfig(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<PersistenceOptions>(configuration.GetSection(PersistenceOptions.PersistenceConfiguration));
        return services;
    }
}
