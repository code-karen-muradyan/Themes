using Themes.API.Enums;
using User.Provider.API.Enums;
using System.Collections.Generic;

namespace Themes.API.Model
{
    public class UserThemeSettingModel
    {
        public ThemeType ThemeType { get; set; }
        public ThemeView ThemeView { get; set; }
        public List<Property> Fields { get; set; }
    }
}