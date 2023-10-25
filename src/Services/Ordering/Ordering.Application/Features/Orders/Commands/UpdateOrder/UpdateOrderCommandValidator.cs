using FluentValidation;

namespace Ordering.Application.Features.Orders.Commands.UpdateOrder
{
    public class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
    {
        public UpdateOrderCommandValidator()
        {
            RuleFor(o => o.UserName).NotEmpty().WithMessage("UserName is required.").NotNull().MaximumLength(50).WithMessage("Username can't be more than 50 word");
            RuleFor(o => o.EmailAddress).NotEmpty().WithMessage("Email is required.").NotNull().EmailAddress().WithMessage("Email is not correct.");
            RuleFor(o => o.TotalPrice).NotEmpty().WithMessage("TotalPrice is required.").NotNull().GreaterThan(0).WithMessage("TotalPrice must be greater than 0");
        }
    }
}
