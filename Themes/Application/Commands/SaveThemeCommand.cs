using Services.Infrastructure;
using Themes.API.Infrastructure.Repositories;
using Themes.API.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Themes.API.Application.Commands
{
    public class SaveThemeCommand : BaseRequest<bool>
    {
        public Guid Id { get; set; }
        public string DisplayName { get; set; }
        public List<Property> Properties { get; set; }
    }
    public class SaveThemeCommandHandler : IRequestHandler<SaveThemeCommand, bool>
    {
        private readonly IThemesRepository _themesRepository;
        public SaveThemeCommandHandler(
            IThemesRepository themesRepository
            )
        {
            _themesRepository = themesRepository ?? throw new ArgumentNullException(nameof(themesRepository));
        }
        public async Task<bool> Handle(SaveThemeCommand request, CancellationToken cancellationToken)
        {
            var teamId = request.GetTeam();
            var userTheme = await _themesRepository.GetUserThemeAsync(teamId, cancellationToken);
            var existTheme = userTheme.Themes.FirstOrDefault(x => x.Id == request.Id);
            if (existTheme == null)
            {
                existTheme = new Theme();
                userTheme.Themes.Add(existTheme);
            }
            existTheme.Id = request.Id;
            existTheme.DisplayName = request.DisplayName;
            existTheme.Properties = request.Properties ?? new List<Property>();
            await _themesRepository.UpdateAsync(userTheme, cancellationToken);
            return true;
        }
    }
}