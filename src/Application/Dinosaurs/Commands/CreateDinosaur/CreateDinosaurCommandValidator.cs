namespace Copilot.Application.Dinosaurs.Commands.CreateDinosaur;

/// <summary>
/// Validator for the CreateDinosaurCommand.
/// </summary>
public class CreateDinosaurCommandValidator : AbstractValidator<CreateDinosaurCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CreateDinosaurCommandValidator"/> class.
    /// </summary>
    public CreateDinosaurCommandValidator()
    {
        RuleFor(v => v.Name)
            .MaximumLength(20)
            .NotEmpty();

        RuleFor(v => v.Sex)
            .Must(sex => sex == "Male" || sex == "Female")
            .WithMessage("Sex must be either 'Male' or 'Female'.");

        RuleFor(v => v.CountryOfOrigin)
            .Matches("^[A-Z]{2}$")
            .WithMessage("Country of origin must be a two-letter capital case code.");

        RuleFor(v => v.NumberOfScales)
            .GreaterThanOrEqualTo(90)
            .Must(number => number % 2 == 0)
            .WithMessage("Number of scales must be even and never less than 90.");
    }
}
