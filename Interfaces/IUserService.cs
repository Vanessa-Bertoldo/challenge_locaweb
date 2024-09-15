using Challenge_Locaweb.Models;

namespace Challenge_Locaweb.Interfaces
{
    public interface IUserService
    {
        public Task<bool> CreateUser(InsertUserModel user);
        public bool DisableOrDisableUser(string id);
        public Task<bool> Login(string email, string senha);
    }
}
