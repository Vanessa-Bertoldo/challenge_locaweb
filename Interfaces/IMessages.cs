using Challenge_Locaweb.Models;

namespace Challenge_Locaweb.Interfaces
{
    public interface IMessages
    {
        public Task<List<MessageModel>> ListMessages();
        public Task InsertMessage(MessageModel message);
    }
}
