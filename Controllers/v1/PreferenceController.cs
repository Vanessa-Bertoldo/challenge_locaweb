using Challenge_Locaweb.Interfaces;
using Challenge_Locaweb.Models;
using Challenge_Locaweb.Services;
using Microsoft.AspNetCore.Mvc;

namespace Challenge_Locaweb.Controllers.v1
{
    [ApiController]
    [Route("[controller]")]
    public class PreferenceController : Controller
    {
        private readonly IPreferenceService _preferenceService;

        public PreferenceController(IPreferenceService preferenceService)
        {
            _preferenceService = preferenceService;
        }

        /// <summary>
        /// Registra as preferências de um usuário.
        /// </summary>
        /// <param name="preference">Preferencias do usuário.</param>
        /// <returns>Status 200</returns>
        [HttpPost("RegistrarPreferencias/{userId}")]
        public async Task<IActionResult> CreatePreference([FromBody] UserPreferencesModel preference, string userId)
        {
            var success = await _preferenceService.CreatePreference(preference, userId);
            return success ? Ok() : BadRequest("Erro ao salvar dados");
        }

        /// <summary>
        /// Pega as preferências de um usuário.
        /// </summary>
        /// <param name="userId">Id do usuário.</param>
        /// <returns>Dados das preferencias de um usuário</returns>
        [HttpGet("buscaPreferencias/{userId}")]
        public async Task<List<UserPreferencesMongoModel>> GetPreferences(string userId)
            => await _preferenceService.GetPreferences(userId);
    }
}
