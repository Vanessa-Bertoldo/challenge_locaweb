using Challenge_Locaweb.Interfaces;
using Challenge_Locaweb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Challenge_Locaweb.Services
{
    public class PreferenceService : IPreferenceService
    {
        private readonly IMongoCollection<UserPreferencesModel> _preferences;
        private readonly IMongoCollection<UserPreferencesMongoModel> _preferencesMongo;

        public PreferenceService(IOptions<MongoDBSettingsModel> settings)
        {
            var mongoClient = new MongoClient(settings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(settings.Value.DatabaseName);
            _preferences = mongoDatabase.GetCollection<UserPreferencesModel>(settings.Value.PreferenceCollection);
            _preferencesMongo = mongoDatabase.GetCollection<UserPreferencesMongoModel>(settings.Value.PreferenceCollection);
        }

        public Task<bool> CreatePreference(UserPreferencesModel preference) =>
            _preferences.InsertOneAsync(preference).ContinueWith(task => task.IsCompletedSuccessfully);

        public async Task<List<UserPreferencesMongoModel>> GetPreferences(string email)
        {
            var filter = Builders<UserPreferencesMongoModel>.Filter.Eq(p => p.Email, email);
            return await _preferencesMongo.Find(filter).ToListAsync();
        }

    }
}
