using Challenge_Locaweb.Models;

namespace Challenge_Locaweb.Interfaces
{
    public interface IMessage
    {
        public Task<List<MessageMongoModel>> ListMessages();
        public Task InsertMessage(MessageModel message);
        public Task<List<MessageMongoModel>> EmailListReceive(string email);
        public Task<List<MessageMongoModel>> EmailListSend(string email);
        public Task<List<MessageMongoModel>> EmailFavoritelList(string email);

        public Task<List<MessageMongoModel>> GetSpans(string email);
    }
}
