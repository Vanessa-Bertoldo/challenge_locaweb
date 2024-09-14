using Challenge_Locaweb.Interfaces;
using Challenge_Locaweb.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Challenge_Locaweb.Services
{
    public class MessageService : IMessages
    {
        private readonly IMongoCollection<MessageModel> _collection;

        public MessageService(IOptions<MongoDBSettingsModel> settings)
        {
            var mongoClient = new MongoClient(settings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(settings.Value.DatabaseName);
            _collection = mongoDatabase.GetCollection<MessageModel>(settings.Value.MessagesCollection);
        }

        public async Task<List<MessageModel>> ListMessages() =>
            await _collection.Find(x => true).ToListAsync();

        public async Task InsertMessage(MessageModel message) =>
            await _collection.InsertOneAsync(message);

    }
}
