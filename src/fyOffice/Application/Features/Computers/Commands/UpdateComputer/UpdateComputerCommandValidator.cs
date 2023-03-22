using FluentValidation;

namespace Application.Features.Computers.Commands.UpdateComputer;

public class UpdateComputerCommandValidator : AbstractValidator<UpdateComputerCommand>
{
    public UpdateComputerCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.Brand).NotEmpty().MinimumLength(2);
    }
}