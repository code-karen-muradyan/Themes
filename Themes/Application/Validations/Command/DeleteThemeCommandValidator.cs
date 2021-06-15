using Services.Infrastructure.Validations;
using Themes.API.Application.Commands;
using Themes.API.Infrastructure.Repositories;
using FluentValidation;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Themes.API.Application.Validations
{
    public class DeleteThemeCommandValidator : BaseTeamRequestValidator<DeleteThemeCommand>
    {
        private readonly IThemesRepository _themesRepository;
        public DeleteThemeCommandValidator(IThemesRepository themesRepository)
        {
            _themesRepository = themesRepository ?? throw new ArgumentNullException(nameof(themesRepository));
            RuleFor(command => command.ThemeId).NotEmpty("Invalid theme id");
            RuleFor(command => command).MustAsync(ExistThemeAsync).WithMessage("Theme not exist");
            RuleFor(command => command).MustAsync(IsNotActiveThemeAsync).WithMessage("Theme is active");
        }

        private async Task<bool> ExistThemeAsync(DeleteThemeCommand command, CancellationToken cancellationToken)
        {
            return await _themesRepository.ExistThemeAsync(command.GetTeam(), command.ThemeId, cancellationToken);
        }
        private async Task<bool> IsNotActiveThemeAsync(DeleteThemeCommand command, CancellationToken cancellationToken)
        {
            return !await _themesRepository.IsActiveThemeAsync(command.GetTeam(), command.ThemeId, cancellationToken);
        }
    }
}