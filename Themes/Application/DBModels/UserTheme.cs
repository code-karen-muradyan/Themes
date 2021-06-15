using Services.Infrastructure;
using Themes.API.Enums;
using User.Provider.API.Enums;
using MongoStore.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Themes.API.Model
{
    public class UserTheme : Entity<Guid>, IMongoDocument, IAggregateRoot
    {
        [JsonIgnore]
        public Guid UserId { get; set; }
        [JsonIgnore]
        public Guid SalePortalId { get; set; }
        public Guid ActiveThemeId { get; set; }
        public ThemeType ThemeType { get; set; }
        public ThemeView ThemeView { get; set; }
        public List<Theme> Themes { get; set; }
        [JsonIgnore]
        public string TraceId { get; set; }
        [JsonIgnore]
        public bool Deleted { get; set; }

        public UserTheme()
        {
            ThemeType = ThemeType.Template_3;
            ThemeView = ThemeView.BoxMenu;
            Themes = new List<Theme>();
        }

        public void SetDefaultThemes(List<Theme> themes)
        {
            foreach (var theme in themes)
            {
                theme.IsDefault = true;
            }
            Themes.AddRange(themes);
        }
    }
}