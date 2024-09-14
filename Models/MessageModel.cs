using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Challenge_Locaweb.Models
{
    public class MessageModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonIgnoreIfNull]
        [BsonElement("messagem")]
        public string Message { get; set; } = null;

        [BsonIgnoreIfNull]
        [BsonElement("nome")]
        public string Name { get; set; } = null;

        [BsonIgnoreIfNull]
        [BsonElement("email")]
        public string Email { get; set; } = null;

    }
}
