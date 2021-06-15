using Services.Infrastructure;
using Themes.API.Model;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Themes.API.Infrastructure.Repositories
{
    public class ThemesRepository : IThemesRepository
    {
        public IUnitOfWork UnitOfWork => _store;
        private readonly BaseMongoStore<UserTheme> _store;
        public ThemesRepository(BaseMongoStore<UserTheme> store)
        {
            _store = store;
        }
        public async Task<bool> CreateAsync(UserTheme userTheme, CancellationToken cancellationToken = default)
        {
            await _store.AddAsync(userTheme, cancellationToken);
            return true;
        }
        public async Task<bool> UpdateAsync(UserTheme userTheme, CancellationToken cancellationToken = default)
        {
            await _store.UpdateAsync(userTheme, cancellationToken);
            return true;
        }
        public async Task<bool> ExistThemeAsync(Guid userId, Guid themeId, CancellationToken cancellationToken = default)
        {
            return await _store.ExistAsync(x => x.UserId == userId && x.Themes != null && x.Themes.Any(y => y.Id == themeId));
        }
        public async Task<bool> IsActiveThemeAsync(Guid userId, Guid themeId, CancellationToken cancellationToken = default)
        {
            return await _store.ExistAsync(x => x.UserId == userId && x.ActiveThemeId == themeId);
        }
        public async Task<UserTheme> GetUserThemeAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            return await _store.GetDocumentAsync(x => x.UserId == userId);
        }
    }
}