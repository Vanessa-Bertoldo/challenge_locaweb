using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Challenge_Locaweb.Models
{
    public class MessageMongoModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("message")]
        public string Message { get; set; }

        [BsonElement("senderName")]
        public string SenderName { get; set; }

        [BsonElement("remententName")]
        public string RemententName { get; set; }

        [BsonElement("Subject")]
        public string Subject { get; set; }

        [BsonElement("isSpam")]
        public bool IsSpam { get; set; }

        [BsonElement("isFavorite")]
        public bool IsFavorite { get; set; }

        [BsonElement("Archives")]
        public List<string> Archives { get; set; } = [];
    }
}
