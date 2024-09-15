using Challenge_Locaweb.Interfaces;
using Challenge_Locaweb.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Challenge_Locaweb.Services
{
    public class MessageService : IMessage
    {
        private readonly IMongoCollection<MessageModel> _collection;
        private readonly IMongoCollection<MessageMongoModel> _collectionMongo;

        public MessageService(IOptions<MongoDBSettingsModel> settings)
        {
            var mongoClient = new MongoClient(settings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(settings.Value.DatabaseName);
            _collection = mongoDatabase.GetCollection<MessageModel>(settings.Value.MessagesCollection);
            _collectionMongo = mongoDatabase.GetCollection<MessageMongoModel>(settings.Value.MessagesCollection);
        }

        public async Task<List<MessageMongoModel>> ListMessages() =>
            await _collectionMongo.Find(x => true).ToListAsync();

        public async Task InsertMessage(MessageModel message) =>
            await _collection.InsertOneAsync(message);

        public async Task<List<MessageMongoModel>> EmailListReceive(string email)
        {
            var filter = Builders<MessageMongoModel>.Filter.Eq(m => m.RemententName, email);
            if (filter == null) return null;
            return await _collectionMongo.Find(filter).ToListAsync();

        }

        public async Task<List<MessageMongoModel>> EmailListSend(string email)
        {
            var filter = Builders<MessageMongoModel>.Filter.Eq(m => m.SenderName, email);
            if (filter == null) return null;
            return await _collectionMongo.Find(filter).ToListAsync();
        }

        public async Task<List<MessageMongoModel>> EmailFavoritelList(string email)
        {
            var filter = Builders<MessageMongoModel>.Filter.And(
                Builders<MessageMongoModel>.Filter.Eq(m => m.SenderName, email),
                Builders<MessageMongoModel>.Filter.Eq(m => m.RemententName, email),
                Builders<MessageMongoModel>.Filter.Eq(m => m.IsFavorite, true)
            );
            if (filter == null) return null;
            return await _collectionMongo.Find(filter).ToListAsync();
        }
    }
}
