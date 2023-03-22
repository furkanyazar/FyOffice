using FluentValidation;

namespace Application.Features.Equipments.Commands.CreateEquipment;

public class CreateEquipmentCommandValidator : AbstractValidator<CreateEquipmentCommand>
{
    public CreateEquipmentCommandValidator()
    {
        RuleFor(c => c.Name).NotEmpty().MinimumLength(2);
        RuleFor(c => c.UnitsInStock).Must(GreaterThanOrEqualTo);
    }

    private bool GreaterThanOrEqualTo(short num) => num >= 0;
}