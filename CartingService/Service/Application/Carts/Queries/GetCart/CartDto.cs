using CartingService.Application.Common.Mappings;
using CartingService.Domain.Entities;

namespace CartingService.Application.Carts.Queries.GetCart;

public class CartDto : IMapFrom<Cart>
{
    public CartDto() => Items = new List<CartItemDto>();

    public string Id { get; set; }
    public IList<CartItemDto> Items { get; set; }
}