using Services.Infrastructure;
using Themes.API.Infrastructure.Repositories;
using Themes.API.Model;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Themes.API.Application.Queries
{
    public class GetUserThemesQuery : BaseQueryRequest<UserTheme>
    {
        private Guid _companyTypeId;
        public void SetCompanyType(Guid companyType) => _companyTypeId = companyType;
        public Guid GetCompanyType() => _companyTypeId;
    }

    public class GetUserThemesQueryHandler : IRequestHandler<GetUserThemesQuery, UserTheme>
    {
        private readonly IThemesRepository _themesRepository;
        public GetUserThemesQueryHandler(IThemesRepository themesRepository)
        {
            _themesRepository = themesRepository ?? throw new ArgumentNullException(nameof(themesRepository));
        }
        public async Task<UserTheme> Handle(GetUserThemesQuery request, CancellationToken cancellationToken)
        {
            var teamId = request.GetTeam();
            var salePortalId = request.GetSalePortal();
            var userTheme = await _themesRepository.GetUserThemeAsync(teamId);
            var salePortalTheme = await _themesRepository.GetUserThemeAsync(salePortalId);
            if (salePortalTheme != null)
            {
                userTheme.SetDefaultThemes(salePortalTheme.Themes);
            }
            return userTheme;
        }
    }
}