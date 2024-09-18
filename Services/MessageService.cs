using Challenge_Locaweb.Interfaces;
using Challenge_Locaweb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Microsoft.AspNetCore.JsonPatch;


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

        public async Task InsertMessage(MessageModel message)
        {
            message.IsSpam = IsMessageSpan(new MessageMongoModel { Message = message.Message });

            await _collection.InsertOneAsync(message);
        }

        public async Task<List<MessageMongoModel>> EmailListReceive(string email)
        {
            var filter = Builders<MessageMongoModel>.Filter.And(
                Builders<MessageMongoModel>.Filter.Eq(m => m.SenderName, email),
                Builders<MessageMongoModel>.Filter.Eq(m => m.IsSpam, false)
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
            var filter = Builders<MessageMongoModel>.Filter.Eq(m => m.RemententName, email);
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
        
        //public async Task<ResultModel> PatchMessageAsync(string id, JsonPatchDocument<MessageMongoModel> patchDoc)
        //{
        //    if (patchDoc == null)
        //    {
        //        return new ResultModel { Success = false, Message = "Valor inválido" };
        //    }

        //    var message = await _collectionMongo.Find(m => m.Id == id).FirstOrDefaultAsync();
        //    if (message == null)
        //    {
        //        return new ResultModel { Success = false, Message = "Mensagem não encontrada" };
        //    }

        //    patchDoc.ApplyTo(message);

        //    var updateDefinition = Builders<MessageMongoModel>.Update
        //        .Set(m => m.IsRead, message.IsRead)
        //        .Set(m => m.IsFavorite, message.IsFavorite)
        //        .Set(m => m.IsDelete, message.IsDelete);

        //    var result = await _collectionMongo.UpdateOneAsync(m => m.Id == id, updateDefinition);

        //    if (result.ModifiedCount == 0)
        //    {
        //        return new ResultModel { Success = false, Message = "Falha ao atualizar dados" };
        //    }

        //    return new ResultModel { Success = true, Message = "Dados atualizados com sucesso" };
        //}



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
            if(string.IsNullOrEmpty(guidMessage))
            {
                return false;
            }

            var filter = Builders<MessageMongoModel>.Filter.Eq(m => m.Id, guidMessage);

            var update = Builders<MessageMongoModel>.Update.Set(m => m.IsFavorite, true);

            var result = await _collectionMongo.UpdateOneAsync(filter, update);

            return result.ModifiedCount > 0;

        }

    }
}
