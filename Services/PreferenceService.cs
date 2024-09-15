using Challenge_Locaweb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Challenge_Locaweb.Services
{
    public class PreferenceService : Controller
    {
        private readonly IMongoCollection<UserModel> _users;

        public PreferenceService(IOptions<MongoDBSettingsModel> settings)
        {
            var mongoClient = new MongoClient(settings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(settings.Value.DatabaseName);
            _users = mongoDatabase.GetCollection<UserModel>(settings.Value.UsersCollection);
        }

        [HttpPost(Name = "Preferences of user")]
        public IActionResult CreatePreference([FromBody] UserPreferencesModel preference)
        {
            
        }
    }
}
