using FluentValidation;
using Vk.Schema;

namespace Vk.Operation.Validation;

public class CardValidator : AbstractValidator<CardRequest>
{
	public CardValidator()
	{
        RuleFor(x => x.Cvv).NotEmpty().Length(3).WithMessage("CVV must be 3 digits.");
        RuleFor(x => x.CardNumber).NotEmpty().WithMessage("Card number is required.");
        RuleFor(x => x.ExpiryDate).NotEmpty().Length(4).WithMessage("Expiry date must be in the format YYYY.");
    }

}
