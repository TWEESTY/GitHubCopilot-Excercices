namespace Copilot.Application.Dinosaurs.Commands.CreateDinosaur;

public class CreateDinosaurCommandValidator : AbstractValidator<CreateDinosaurCommand>
{
    public CreateDinosaurCommandValidator()
    {
        RuleFor(v => v.Name)
            .MaximumLength(20)
            .NotEmpty();

        RuleFor(v => v.Sex)
            .Must(sex => sex == "Male" || sex == "Female")
            .WithMessage("Sex must be either 'Male' or 'Female'.");
    }
}
