using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace Themes.API.Model
{
    public class Theme
    {
        public Guid Id { get; set; }
        public string DisplayName { get; set; }
        public List<Property> Properties { get; set; }
        [BsonIgnore]
        public bool IsDefault { get; set; }
        public Theme()
        {
            Properties = new List<Property>();
        }
    }
}