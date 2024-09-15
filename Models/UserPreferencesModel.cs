using MongoDB.Bson.Serialization.Attributes;

namespace Challenge_Locaweb.Models
{
    public class UserPreferencesModel
    {
        [BsonElement("notificationEnabled")]
        public bool NotificationEnabled { get; set; }

        [BsonElement("autoReply")]
        public bool AutoReply { get; set; }

        [BsonElement("labels")]
        public List<string> Labels { get; set; }
    }
}
