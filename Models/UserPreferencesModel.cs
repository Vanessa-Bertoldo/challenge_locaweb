using MongoDB.Bson.Serialization.Attributes;

namespace Challenge_Locaweb.Models
{
    public class UserPreferencesModel
    {
        [BsonElement("email")]
        public string Email { get; set; }
        [BsonElement("notificationEnabled")]
        public bool? NotificationEnabled { get; set; } = true;

        [BsonElement("Theme")]
        public string? Theme { get; set; }

        [BsonElement("userId")]
        public string? UserId { get; set; }
    }
}
