using Challenge_Locaweb.Interfaces;
using Challenge_Locaweb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Microsoft.AspNetCore.JsonPatch;


namespace Challenge_Locaweb.Services
{
    public class MessageService : IMessageService
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

        public async Task InsertMessage(MessageModel message)
        {
            message.IsSpam = IsMessageSpan(new MessageMongoModel { Message = message.Message });

            await _collection.InsertOneAsync(message);
        }

        public async Task<List<MessageMongoModel>> EmailListReceive(string email)
        {
            var filter = Builders<MessageMongoModel>.Filter.And(
                Builders<MessageMongoModel>.Filter.Eq(m => m.SenderName, email),
                Builders<MessageMongoModel>.Filter.Eq(m => m.IsSpam, false),
                Builders<MessageMongoModel>.Filter.Eq(m => m.DataEvent, null)
            ); 
            if (filter == null) return null;
            return await _collectionMongo.Find(filter).ToListAsync();

        }

        public async Task<ActionResult<List<MessageMongoModel>>> EmailListBin(string email)
        {
            var filter = Builders<MessageMongoModel>.Filter.And(
                         Builders<MessageMongoModel>.Filter.Eq(m => m.IsDelete, true),
                         Builders<MessageMongoModel>.Filter.Or(
                             Builders<MessageMongoModel>.Filter.Eq(m => m.SenderName, email),
                             Builders<MessageMongoModel>.Filter.Eq(m => m.RemententName, email)
                         )
                     );


            if (filter == null) return null;
            return await _collectionMongo.Find(filter).ToListAsync();
        }

        public async Task<List<MessageMongoModel>> EmailListSend(string email)
        {
            var filter = Builders<MessageMongoModel>.Filter.And(
                Builders<MessageMongoModel>.Filter.Eq(m => m.RemententName, email),
                Builders<MessageMongoModel>.Filter.Eq(m => m.DataEvent, null)
            );
            if (filter == null) return null;
            return await _collectionMongo.Find(filter).ToListAsync();
        }

        public async Task<List<MessageMongoModel>> EmailFavoritelList(string email)
        {
            var filter = Builders<MessageMongoModel>.Filter.And(
                         Builders<MessageMongoModel>.Filter.Eq(m => m.IsFavorite, true),
                         Builders<MessageMongoModel>.Filter.Or(
                             Builders<MessageMongoModel>.Filter.Eq(m => m.SenderName, email),
                             Builders<MessageMongoModel>.Filter.Eq(m => m.RemententName, email)
                         ));
            if (filter == null) return null;
            return await _collectionMongo.Find(filter).ToListAsync();
        }
        
        public async Task<List<MessageMongoModel>> GetSpans(string email)
        {
            var messages = await _collectionMongo.Find(m => m.SenderName == email).ToListAsync();

            return new List<MessageMongoModel>();

        }

        private bool IsMessageSpan(MessageMongoModel message)
        {
            string[] spamKeywords = {
                "free",
                "offer",
                "win",
                "prize",
                "click here",
                "click aqui",
                "buy now",
                "discount",
                "limited time",
                "exclusive offer",
                "congratulations",
                "no cost",
                "risk-free",
                "money-back",
                "100% free",
                "guaranteed",
                "urgent",
                "claim now",
                "cash bonus",
                "act now",
                "unsubscribe",
                "credit card required",
                "instant access",
                "cheap",
                "fast cash",
                "get paid",
                "amazing deal",
                "low price",
                "grátis",
                "oferta",
                "ganhe",
                "prêmio",
                "clique aqui",
                "compre agora",
                "desconto",
                "tempo limitado",
                "oferta exclusiva",
                "parabéns",
                "sem custo",
                "sem risco",
                "garantia de devolução",
                "100% grátis",
                "garantido",
                "urgente",
                "resgate agora",
                "bônus em dinheiro",
                "aja agora",
                "cancelar inscrição",
                "cartão de crédito necessário",
                "acesso imediato",
                "barato",
                "dinheiro rápido",
                "seja pago",
                "oferta incrível",
                "preço baixo"
            };


            foreach (var keyword in spamKeywords)
            {
                if (message.Message.Contains(keyword, StringComparison.OrdinalIgnoreCase)) 
                {
                    return true;
                }
            }

            if (CountMessagesFromSender(message.SenderName) > 3)
            {
                return true;
            }

            return false;
        }


        private int CountMessagesFromSender(string sender)
        {
            var filter = Builders<MessageModel>.Filter.Eq(m => m.RemententName, sender);
            int count = (int)_collection.CountDocuments(filter);

            return count;
        }

        public async Task<bool> FavoriteMessage(string guidMessage)
        {

            var filter = Builders<MessageMongoModel>.Filter.Eq(m => m.Id, guidMessage);
            var message = await _collectionMongo.Find(filter).FirstOrDefaultAsync();
            var result = await _collectionMongo.UpdateOneAsync(
                filter, Builders<MessageMongoModel>.Update.Set(m => m.IsFavorite, !message.IsFavorite));

            return result.ModifiedCount > 0;

        }

        public async Task<bool> DeleteMessage(string guidMessage)
        {
            var filter = Builders<MessageMongoModel>.Filter.Eq(m => m.Id, guidMessage);
            var result = await _collectionMongo.DeleteOneAsync(filter);
            return result.DeletedCount > 0;
        }

        public async Task<ActionResult<List<MessageMongoModel>>> EmailListEvents(string email)
        {
            var filter = Builders<MessageMongoModel>.Filter.Ne(m => m.DataEvent, null);

            return await _collectionMongo.Find(filter).ToListAsync();
        }
    }
}
