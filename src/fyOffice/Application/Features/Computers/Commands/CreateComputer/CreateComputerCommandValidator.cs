using FluentValidation;

namespace Application.Features.Computers.Commands.CreateComputer;

public class CreateComputerCommandValidator : AbstractValidator<CreateComputerCommand>
{
    public CreateComputerCommandValidator()
    {
        RuleFor(c => c.Brand).NotEmpty().MinimumLength(2);
    }
}