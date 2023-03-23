﻿using AutoMapper;
using CartingService.Application.Common.Mappings;
using CartingService.Domain.Entities;

namespace CartingService.Application.Carts.Queries.GetCart;
public class CartItemDto : IMapFrom<CartItem>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string CurrencyCode { get; set; }
    public float Price { get; set; }
    public int Quantity { get; set; }
    public string? ImageUri { get; set; }
    public string? ImageAltText { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<CartItem, CartItemDto>()
            .ForMember(d => d.ImageAltText, opt => opt.MapFrom(s => s.WebImage != null ? s.WebImage.AltText : null))
            .ForMember(d => d.ImageUri, opt => opt.MapFrom(s => s.WebImage != null ? s.WebImage.Uri : null))
            .ForMember(d => d.CurrencyCode, opt => opt.MapFrom(s => s.Currency.Code));
    }
}
