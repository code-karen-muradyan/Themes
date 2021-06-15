using Services.Infrastructure;
using Themes.API.Model;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Themes.API.Infrastructure.Repositories
{
    public interface IThemesRepository : IRepository<UserTheme>
    {
        Task<bool> CreateAsync(UserTheme userTheme, CancellationToken cancellationToken = default);
        Task<bool> UpdateAsync(UserTheme userTheme, CancellationToken cancellationToken = default);
        Task<bool> ExistThemeAsync(Guid userId, Guid themeId, CancellationToken cancellationToken = default);
        Task<bool> IsActiveThemeAsync(Guid userId, Guid themeId, CancellationToken cancellationToken = default);
        Task<UserTheme> GetUserThemeAsync(Guid userId, CancellationToken cancellationToken = default);
    }
}