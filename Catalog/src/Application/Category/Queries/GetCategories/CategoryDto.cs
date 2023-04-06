﻿using AutoMapper;
using Catalog.Application.Common.Mappings;
using Catalog.Domain.Entities;

namespace Catalog.Application.Categorys.Queries.GetCategories;

public class CategoryDto : IMapFrom<Category>
{
    public int Id { get; set; }

    public string Name { get; set; }

    public Uri? ImageUrl { get; set; }

    public int? ParentCategoryId { get; set; }

    public string? ParentCategoryName { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Category, CategoryDto>()
            .ForMember(d => d.ParentCategoryName, opt => opt.MapFrom(s => s.ParentCategory != null ? s.ParentCategory.Name : null));
    }
}