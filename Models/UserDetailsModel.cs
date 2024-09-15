using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Challenge_Locaweb.Models
{
    public class UserDetailsModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("personalData")]
        public UserModel PersonalData { get; set; }

        [BsonElement("preferences")]
        public UserPreferencesModel Preferences { get; set; }
    }
}
