using FluentValidation;

namespace CartingService.Application.Carts.Queries.GetCart;
public class GetCartQueryCommandValidator : AbstractValidator<GetCartQuery>
{
    public GetCartQueryCommandValidator()
    {
        RuleFor(v => v.CartId)
            .NotEmpty();
    }
}
