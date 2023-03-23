﻿using AutoMapper;
using CartingService.Application.Common.Interfaces;
using CartingService.Domain.Entities;
using MediatR;

namespace CartingService.Application.Carts.Queries.GetCart;
public record GetCartQuery : IRequest<CartDto>
{
    public string CartId { get; init; }
}

public class GetCartQueryHandler : IRequestHandler<GetCartQuery, CartDto>
{
    private readonly ICartingDbContext _context;
    private readonly IMapper _mapper;

    public GetCartQueryHandler(ICartingDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<CartDto> Handle(GetCartQuery request, CancellationToken cancellationToken)
    {
        var cart = _context.Get<Cart>(request.CartId);

        return _mapper.Map<Cart, CartDto>(cart);
    }
}
