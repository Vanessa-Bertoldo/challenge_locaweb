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
        public bool? IsSpam { get; set; }

        [BsonElement("isFavorite")]
        public bool? IsFavorite { get; set; }

        [BsonElement("isDelete")]
        public bool? IsDelete { get; set; }

        [BsonElement("isRead")]
        public bool? IsRead { get; set; } 

        [BsonElement("Archives")]
        public List<string>? Archives { get; set; }

        [BsonElement("DataEvent")]
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime? DataEvent { get; set; }

        [BsonElement("DataRegister")]
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime DataRegister { get; set; } = DateTime.Now;

    }
}
