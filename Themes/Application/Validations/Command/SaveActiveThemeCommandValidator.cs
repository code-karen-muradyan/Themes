using Services.Infrastructure.Validations;
using Themes.API.Application.Commands;
using Themes.API.Infrastructure.Repositories;
using FluentValidation;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Themes.API.Application.Validations
{
    public class SaveActiveThemeCommandValidator: BaseTeamRequestValidator<SaveActiveThemeCommand>
    {
        private readonly IThemesRepository _themesRepository;
        public SaveActiveThemeCommandValidator(IThemesRepository themesRepository)
        {
            _themesRepository = themesRepository ?? throw new ArgumentNullException(nameof(themesRepository));
            RuleFor(command => command.ThemeId).NotEmpty("Invalid theme id");
            RuleFor(command => command).MustAsync(ExistThemeAsync).WithMessage("Theme not exist");
        }

        private async Task<bool> ExistThemeAsync(SaveActiveThemeCommand command, CancellationToken cancellationToken)
        {
            return await _themesRepository.ExistThemeAsync(command.GetTeam(), command.ThemeId, cancellationToken);
        }
    }
}