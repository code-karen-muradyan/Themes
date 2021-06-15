using Services.Infrastructure.Validations;
using Themes.API.Application.Commands;
using FluentValidation;

namespace Themes.API.Application.Validations
{
    public class SaveThemeCommandValidator: BaseTeamRequestValidator<SaveThemeCommand>
    {
        public SaveThemeCommandValidator()
        {
            RuleFor(command => command.Id).NotEmpty("Invalid theme id");
            RuleFor(command => command.DisplayName).NotNull().NotEmpty()
                .Must(x => !string.IsNullOrEmpty(x?.Trim()))
                .WithMessage("Invalid display name");
        }
    }
}