﻿using MongoDB.Bson.Serialization.Attributes;

namespace Challenge_Locaweb.Models
{
    public class UserPreferencesModel
    {
        [BsonElement("email")]
        public string Email { get; set; }
        [BsonElement("notificationEnabled")]
        public bool NotificationEnabled { get; set; }

        [BsonElement("Theme")]
        public string Theme { get; set; }

        [BsonElement("backgroundImage")]
        public string BackgroundImage { get; set; }

        [BsonElement("backgroundColor")]
        public string BackgroundColor { get; set; }
    }
}
