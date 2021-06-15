using Services.Infrastructure.Validations;
using Themes.API.Application.Commands;
using FluentValidation;

namespace Themes.API.Application.Validations
{
    public class CreateActiveThemeCommandValidator : BaseTeamRequestValidator<CreateActiveThemeCommand>
    {
        public CreateActiveThemeCommandValidator()
        {
            RuleFor(command => command.Id).NotEmpty("Invalid active theme id");
        }
    }
}