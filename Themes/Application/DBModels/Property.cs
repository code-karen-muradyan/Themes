using Themes.API.Enums;
using System;

namespace Themes.API.Model
{
    public class Property
    {
        public Guid Id { get; set; }
        public string DisplayName { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public PropertyType PropertyType { get; set; }
    }
}