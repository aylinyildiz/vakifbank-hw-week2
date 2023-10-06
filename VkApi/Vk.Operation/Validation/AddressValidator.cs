using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vk.Schema;

namespace Vk.Operation.Validation;

public class AddressValidator : AbstractValidator<AddressRequest>
{
	public AddressValidator()
	{
		RuleFor(x => x.AddressLine1).NotEmpty().WithMessage("Address is required");
	}
}
