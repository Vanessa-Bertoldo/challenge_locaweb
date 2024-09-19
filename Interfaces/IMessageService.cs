using Challenge_Locaweb.Models;
using Microsoft.AspNetCore.Mvc;

namespace Challenge_Locaweb.Interfaces
{
    public interface IMessageService
    {
        public Task<List<MessageMongoModel>> ListMessages();
        public Task InsertMessage(MessageModel message);
        public Task<List<MessageMongoModel>> EmailListReceive(string email);
        public Task<List<MessageMongoModel>> EmailListSend(string email);
        public Task<List<MessageMongoModel>> EmailFavoritelList(string email);

        public Task<List<MessageMongoModel>> GetSpans(string email);
        public Task<List<MessageMongoModel>> EmailListBin(string email);
        public Task<bool> FavoriteMessage(string guidMessage);
        public Task<bool> DeleteMessage(string guidMessage);
        public Task<List<MessageMongoModel>> EmailListEvents(string email);
        public Task<bool> deleteEvent(string guidMessage);

    }
}
