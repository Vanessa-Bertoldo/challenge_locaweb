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
        /// <param name="email">O email do usuário.</param>
        /// <returns>Status 200</returns>
        [HttpPost("RegistrarPreferencias")]
        public async Task<IActionResult> CreatePreference(UserPreferencesModel preference)
        {
            var success = await _preferenceService.CreatePreference(preference);
            return success ? Ok() : BadRequest("Erro ao salvar dados");
        }

        /// <summary>
        /// Pega as preferências de um usuário.
        /// </summary>
        /// <param name="email">O email do usuário.</param>
        /// <returns>Dados das preferencias de um usuário</returns>
        [HttpGet("buscaPreferencias")]
        public async Task<List<UserPreferencesModel>> GetPreferences(string email)
            => await _preferenceService.GetPreferences(email);
    }
}
