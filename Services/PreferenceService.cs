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

        public async Task<bool> CreatePreference(UserPreferencesMongoModel preference, string userId)
        {
            var filter = Builders<UserPreferencesMongoModel>.Filter.Eq(m => m.UserId, userId);
            var pref = await _preferencesMongo.Find(filter).FirstOrDefaultAsync();

            if (pref.Theme.ToLower() == "dark")
            {
                preference.UserId = userId;
                await _preferencesMongo.DeleteOneAsync(filter);
                return true;
            }
            else
            {
                preference.UserId = userId;
                await _preferencesMongo.InsertOneAsync(preference);
                return true;
            }
        }

        public async Task<List<UserPreferencesMongoModel>> GetPreferences(string userId)
        {
            var filter = Builders<UserPreferencesMongoModel>.Filter.Eq(p => p.UserId, userId);
            return await _preferencesMongo.Find(filter).ToListAsync();
        }

    }
}
