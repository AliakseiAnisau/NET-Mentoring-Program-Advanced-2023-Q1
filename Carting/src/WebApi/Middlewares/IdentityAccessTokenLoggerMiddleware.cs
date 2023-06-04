using System.IdentityModel.Tokens.Jwt;

namespace Carting.WebApi.Middlewares;

public class IdentityAccessTokenLoggerMiddleware : IMiddleware
{
    private readonly ILogger<IdentityAccessTokenLoggerMiddleware> _logger;

    public IdentityAccessTokenLoggerMiddleware(ILogger<IdentityAccessTokenLoggerMiddleware> logger)
    {
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var accessToken = context.Request.Headers["Authorization"].ToString();
        if (!string.IsNullOrEmpty(accessToken))
        {
            var token = accessToken.Replace("Bearer ", "");
            var jwttoken = new JwtSecurityTokenHandler().ReadJwtToken(token);
            var sub = jwttoken.Claims.FirstOrDefault(cl => cl.Type == "sub")?.Value;
            _logger.LogInformation($"Identity access token for UserId: {sub}: AccessToken: {accessToken}");
        }

        await next(context);
    }
}
