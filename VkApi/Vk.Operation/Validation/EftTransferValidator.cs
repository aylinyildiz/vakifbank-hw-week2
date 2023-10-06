using FluentValidation;
using Vk.Schema;

namespace Vk.Operation.Validation;

public class EftTransferValidator : AbstractValidator<EftTransferRequest>
{
    public EftTransferValidator()
    {
        RuleFor(x => x.FromAccountId).NotEmpty().GreaterThan(0).WithMessage("FromAccount is required.");
        RuleFor(x => x.ToAccountId).NotEmpty().GreaterThan(0).WithMessage("ToAccount is required.");
        RuleFor(x => x.Amount).NotEmpty().GreaterThan(0).WithMessage("Amount is required.");
        RuleFor(x => x.Description).NotEmpty().MinimumLength(10).WithMessage("Description is required.");

    }
}
