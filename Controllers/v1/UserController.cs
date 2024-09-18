using Challenge_Locaweb.Interfaces;
using Challenge_Locaweb.Models;
using Challenge_Locaweb.Services;
using Microsoft.AspNetCore.Mvc;

namespace Challenge_Locaweb.Controllers.v1
{

    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Cria um novo usuário.
        /// </summary>
        /// <param name="user">O modelo do usuário a ser criado.</param>
        /// <returns>Um status indicando o resultado da operação.</returns>
        [HttpPost("Criar")]
        public async Task<IActionResult> CreateUser(InsertUserModel user)
        {
            var success = await _userService.CreateUser(user);
            if (!success)
                return BadRequest("Usuário já cadastrado");
            return Ok("Usuário cadastrado com sucesso");
        }

        /// <summary>
        /// Ativa ou desativa um usuário com base no email.
        /// </summary>
        /// <param name="email">O email do usuário a ser ativado ou desativado.</param>
        /// <returns>Um status indicando o resultado da operação.</returns>
        [HttpPut("desativarAtivar/{email}")]
        public IActionResult DisableOrDisableUser(string email)
        {
            var result = _userService.DisableOrDisableUser(email);
            return result ? Ok("Usuário desativado/ativado com sucesso") : NotFound("Usuário não encontrado");
        }

        /// <summary>
        /// Faz login do usuário com base no email e senha fornecidos.
        /// </summary>
        /// <param name="loginModel">O modelo contendo o email e a senha para login.</param>
        /// <returns>Um status indicando o resultado da operação.</returns>
        [HttpPost("login")] 
        public async Task<IActionResult> Login([FromBody] LoginModel login)
        {
            if (login == null || string.IsNullOrEmpty(login.Email) || string.IsNullOrEmpty(login.Password))
                return BadRequest("Email e senha são obrigatórios.");

            var result = await _userService.Login(login.Email, login.Password);

            return result ? Ok("Usuário logado com sucesso") : NotFound("Usuário ou senha inválidos");
        }

    }
}
