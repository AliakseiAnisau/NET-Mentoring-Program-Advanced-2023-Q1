using FluentValidation;

namespace CartingService.Application.Carts.Commands.RemoveItemFromCart;
public class RemoveItemFromCartCommandValidator : AbstractValidator<RemoveItemFromCartCommand>
{
    public RemoveItemFromCartCommandValidator()
    {
        RuleFor(v => v.CartId)
            .NotEmpty();
    }
}
