using Services.Infrastructure;
using Themes.API.Enums;
using Themes.API.Infrastructure.Repositories;
using Themes.API.Model;
using User.Provider.API.Enums;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Themes.API.Application.Commands
{
    public class CreateActiveThemeCommand : BaseRequest<bool>
    {
        public Guid Id { get; set; }
        public ThemeType Type { get; set; }
        public ThemeView View { get; set; }
    }
    public class CreateActiveThemeCommandHandler : IRequestHandler<CreateActiveThemeCommand, bool>
    {
        private readonly IThemesRepository _themesRepository;
        public CreateActiveThemeCommandHandler(IThemesRepository themesRepository)
        {
            _themesRepository = themesRepository ?? throw new ArgumentNullException(nameof(themesRepository));
        }
        public async Task<bool> Handle(CreateActiveThemeCommand request, CancellationToken cancellationToken)
        {
            var teamId = request.GetTeam();
            var userTheme = new UserTheme();
            userTheme.Id = request.Id;
            userTheme.UserId = teamId;
            userTheme.ThemeType = request.Type;
            userTheme.ThemeView = request.View;
            await _themesRepository.CreateAsync(userTheme);
            return true;
        }
    }
}