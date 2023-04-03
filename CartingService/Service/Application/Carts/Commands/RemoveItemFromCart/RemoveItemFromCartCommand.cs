using CartingService.Application.Common.Exceptions;
using CartingService.Application.Common.Interfaces;
using CartingService.Domain.Entities;
using MediatR;

namespace CartingService.Application.Carts.Commands.RemoveItemFromCart;
public record RemoveItemFromCartCommand : IRequest<string>
{
    public string CartId { get; init; }
    public int Id { get; init; }
}

public class RemoveItemFromCartCommandHandler : IRequestHandler<RemoveItemFromCartCommand, string>
{
    private readonly ICartingDbContext _context;

    public RemoveItemFromCartCommandHandler(ICartingDbContext context)
    {
        _context = context;
    }


    Task<string> IRequestHandler<RemoveItemFromCartCommand, string>.Handle(RemoveItemFromCartCommand request, CancellationToken cancellationToken)
    {
        var cart = _context.Get<Cart>(request.CartId);
        if (cart == null)
            throw new NotFoundException(typeof(Cart).Name, request.CartId);

        var cartItem = cart.Items.FirstOrDefault(i => i.Id == request.Id);

        if (cartItem == null)
            throw new NotFoundException(typeof(CartItem).Name, request.Id);

        cart.Items.Remove(cartItem);

        if (cart.Items.Any())
        {
            _context.Delete<Cart>(cart.Id);
        }
        else 
        {
            _context.Update(cart);
        }

        return Task.FromResult(cart.Id);
    }
}
