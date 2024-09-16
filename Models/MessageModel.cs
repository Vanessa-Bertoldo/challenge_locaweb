using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Security.Cryptography.Xml;

namespace Challenge_Locaweb.Models
{
    public class MessageModel
    {
        [BsonElement("message")]
        public string Message { get; set; }

        [BsonElement("senderName")]
        public string SenderName { get; set; }

        [BsonElement("remententName")]
        public string RemententName { get; set; }

        [BsonElement("Subject")]
        public string Subject { get; set; }

        [BsonElement("isSpam")]
        public bool IsSpam { get; set; } = false;

        [BsonElement("isFavorite")]
        public bool IsFavorite { get; set; } = false;

        [BsonElement("isDelete")]
        public bool IsDelete { get; set; } = false;

        [BsonElement("isRead")]
        public bool IsRead { get; set; } = false;

        [BsonElement("Archives")]
        public List<string> Archives { get; set; } = [];

    }
}
