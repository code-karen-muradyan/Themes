using Services.Infrastructure.Validations;
using Themes.API.Application.Queries;

namespace Themes.API.Application.Validations
{
    public class GetActiveUserThemeQueryValidator : BaseTeamRequestValidator<GetActiveUserThemeQuery>
    {
        public GetActiveUserThemeQueryValidator()
        {
        }
    }
}