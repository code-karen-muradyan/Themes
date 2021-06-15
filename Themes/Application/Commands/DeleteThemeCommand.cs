using Services.Infrastructure;
using Themes.API.Infrastructure.Repositories;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Themes.API.Application.Commands
{
    public class DeleteThemeCommand : BaseRequest<bool>
    {
        public Guid ThemeId { get; set; }
    }
    public class DeleteThemeCommandHandler : IRequestHandler<DeleteThemeCommand, bool>
    {
        private readonly IThemesRepository _themesRepository;
        public DeleteThemeCommandHandler(IThemesRepository themesRepository)
        {
            _themesRepository = themesRepository ?? throw new ArgumentNullException(nameof(themesRepository));
        }
        public async Task<bool> Handle(DeleteThemeCommand request, CancellationToken cancellationToken)
        {
            var teamId = request.GetTeam();
            var userTheme = await _themesRepository.GetUserThemeAsync(teamId, cancellationToken);
            var existTheme = userTheme.Themes.FirstOrDefault(x => x.Id == request.ThemeId);
            userTheme.Themes.Remove(existTheme);
            await _themesRepository.UpdateAsync(userTheme, cancellationToken);
            return true;
        }
    }
}