using FluentValidation;

namespace Application.Features.Equipments.Commands.UpdateEquipment;

public class UpdateEquipmentCommandValidator : AbstractValidator<UpdateEquipmentCommand>
{
    public UpdateEquipmentCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.Name).NotEmpty().MinimumLength(2);
        RuleFor(c => c.UnitsInStock).Must(GreaterThanOrEqualTo);
    }

    private bool GreaterThanOrEqualTo(short num) => num >= 0;
}