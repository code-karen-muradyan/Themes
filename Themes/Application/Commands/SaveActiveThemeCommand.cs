using Services.Infrastructure;
using Themes.API.Enums;
using Themes.API.Infrastructure.Repositories;
using User.Provider.API.Enums;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Themes.API.Application.Commands
{
    public class SaveActiveThemeCommand : BaseRequest<bool>
    {
        public Guid ThemeId { get; set; }
        public ThemeType Type { get; set; }
        public ThemeView View { get; set; }
    }

    public class SaveActiveThemeCommandHandler : IRequestHandler<SaveActiveThemeCommand, bool>
    {
        private readonly IThemesRepository _themesRepository;
        public SaveActiveThemeCommandHandler(IThemesRepository themesRepository)
        {
            _themesRepository = themesRepository ?? throw new ArgumentNullException(nameof(themesRepository));
        }
        public async Task<bool> Handle(SaveActiveThemeCommand request, CancellationToken cancellationToken)
        {
            var teamId = request.GetTeam();
            var userTheme = await _themesRepository.GetUserThemeAsync(teamId, cancellationToken);
            userTheme.ThemeType = request.Type;
            userTheme.ThemeView = request.View;
            userTheme.ActiveThemeId = request.ThemeId;
            await _themesRepository.UpdateAsync(userTheme);
            return true;
        }
    }
}