using Challenge_Locaweb.Models;
using Microsoft.AspNetCore.Mvc;

namespace Challenge_Locaweb.Interfaces
{
    public interface IPreferenceService
    {
        public Task<bool> CreatePreference(UserPreferencesModel preference);
        public Task<List<UserPreferencesMongoModel>> GetPreferences(string email);
    }
}
