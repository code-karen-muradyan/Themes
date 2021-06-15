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
    public class GetActiveUserThemeQuery : BaseQueryRequest<ActiveUserThemeReadModel>
    {
    }
    public class GetActiveUserThemeQueryHandler : IRequestHandler<GetActiveUserThemeQuery, ActiveUserThemeReadModel>
    {
        private readonly IThemesRepository _themesRepository;
        public GetActiveUserThemeQueryHandler(IThemesRepository themesRepository)
        {
            _themesRepository = themesRepository ?? throw new ArgumentNullException(nameof(themesRepository));
        }
        public async Task<ActiveUserThemeReadModel> Handle(GetActiveUserThemeQuery request, CancellationToken cancellationToken)
        {
            var userId = request.GetTeam();
            var userTheme = await _themesRepository.GetUserThemeAsync(userId, cancellationToken);
            if (userTheme == null)
            {
                return new ActiveUserThemeReadModel();
            }
            var themes = userTheme.Themes;
            var activeTheme = userTheme.Themes.FirstOrDefault(x => x.Id == userTheme.ActiveThemeId);
            return new ActiveUserThemeReadModel
            {
                ThemeType = userTheme.ThemeType,
                ThemeView = userTheme.ThemeView,
                Fields = activeTheme.Properties
            };
        }
    }
}