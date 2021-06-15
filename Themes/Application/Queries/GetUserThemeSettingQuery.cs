using Services.Infrastructure;
using Themes.API.Infrastructure.Repositories;
using Themes.API.Model;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Themes.API.Application.Queries
{
    public class GetUserThemeSettingQuery : BaseQueryRequest<UserThemeSettingModel>
    {

    }
    public class GetUserThemeSettingQueryHandler : IRequestHandler<GetUserThemeSettingQuery, UserThemeSettingModel>
    {
        private readonly IThemesRepository _themesRepository;
        public GetUserThemeSettingQueryHandler(IThemesRepository themesRepository)
        {
            _themesRepository = themesRepository ?? throw new ArgumentNullException(nameof(themesRepository));
        }
        public async Task<UserThemeSettingModel> Handle(GetUserThemeSettingQuery request, CancellationToken cancellationToken)
        {
            var teamId = request.GetTeam();
            var userTheme = await _themesRepository.GetUserThemeAsync(teamId, cancellationToken);
            var activeTheme = userTheme.Themes.FirstOrDefault(x => x.Id == userTheme.ActiveThemeId);
            return new UserThemeSettingModel
            {
                ThemeType = userTheme.ThemeType,
                ThemeView = userTheme.ThemeView,
                Fields = activeTheme.Properties
            };
        }
    }
}