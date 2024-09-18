using Challenge_Locaweb.Interfaces;
using Challenge_Locaweb.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using BCrypt.Net;

namespace Challenge_Locaweb.Services
{
    public class UserService : IUserService
    {

        private readonly IMongoCollection<UserModel> _collection;
        private readonly IMongoCollection<InsertUserModel> _collectionNotId;

        public UserService(IOptions<MongoDBSettingsModel> settings)
        {
            var mongoClient = new MongoClient(settings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(settings.Value.DatabaseName);
            _collection = mongoDatabase.GetCollection<UserModel>(settings.Value.UsersCollection);
            _collectionNotId = mongoDatabase.GetCollection<InsertUserModel>(settings.Value.UsersCollection);
        }

        public async Task<bool> CreateUser(InsertUserModel user)
        {
            var userExists = await this.userExists(user);
            if (userExists) return false;
            user.Password = HashPassword(user.Password);
            await _collectionNotId.InsertOneAsync(user);
            return true;
        }


        public bool DisableOrDisableUser(string email)
        {
            var filter = Builders<UserModel>.Filter.Eq(u => u.Email, email);
            var user = _collection.Find(filter).FirstOrDefault();
            if (user == null) return false;
            var update = Builders<UserModel>.Update.Set(u => u.IsActive, !user.IsActive);
            var result = _collection.UpdateOne(filter, update);
            return result.ModifiedCount > 0;
        }


        public async Task<bool> Login(string email, string senha)
        {
            var filter = Builders<UserModel>.Filter.Eq(u => u.Email, email);
            var user = await _collection.Find(filter).FirstOrDefaultAsync();

            if (user == null)
                return false;

            var verifyLogin = await VerifyPassword(senha, user.Password);
            return verifyLogin;
        }

        private string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public async Task<bool> VerifyPassword(string enteredPassword, string hashedPassword)
        {
            return  BCrypt.Net.BCrypt.Verify(enteredPassword, hashedPassword);
        }

        public async Task<bool> userExists(InsertUserModel user)
        {
            var filter = Builders<UserModel>.Filter.Eq(u => u.Email, user.Email);
            var userExists = _collection.Find(filter).FirstOrDefault();
            if (userExists != null) return true;
            return false;
        }

    }
}
