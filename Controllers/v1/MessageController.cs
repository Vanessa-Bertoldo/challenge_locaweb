using Challenge_Locaweb.Models;
using Challenge_Locaweb.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace Challenge_Locaweb.Controllers.v1
{
    [ApiController]
    [Route("[controller]")]
    public class MessageController : ControllerBase
    {

        private readonly MessageService _messageService;

        public MessageController(MessageService messageService)
        {
            _messageService = messageService;
        }

        /// <summary>
        /// Lista emails excluidos de um usuário
        /// </summary>
        /// <param name="message">Lista de emails excluídos.</param>
        /// <returns>Status 200.</returns>
        [HttpGet("lixeira/{email}")]
        public async Task<ActionResult<List<MessageMongoModel>>> ListEmailsBin(string email)
        {
            var result = await _messageService.EmailListBin(email);
            if (result == null)
            {
                return BadRequest("Endereço de email inválido");
            }
            return Ok(result);
        }

        /// <summary>
        /// Lista os emails recebidos de um usuário
        /// </summary>
        /// <param name="email">Email a ser procurado as mensagens.</param>
        /// <returns>Status 200.</returns>
        [HttpGet("buscaRecebidos/{email}")]
        public async Task<ActionResult<List<MessageMongoModel>>> ListEmailsReceived(string email)
        {
            var result = await _messageService.EmailListReceive(email);
            if (result == null)
            {
                return BadRequest("Endereço de email inválido");
            }
            return Ok(result);
        }

        /// <summary>
        /// Lista os emails enviados de um usuário
        /// </summary>
        /// <param name="email">Email a ser procurado as mensagens.</param>
        /// <returns>Status 200.</returns>
        [HttpGet("buscaEnviados/{email}")]
        public async Task<ActionResult<List<MessageMongoModel>>> ListEmailsSent(string email)
        {
            var result = await _messageService.EmailListSend(email);
            if (result == null)
            {
                return BadRequest("Endereço de email inválido");
            }
            return Ok(result);
        }

        /// <summary>
        /// Lista os emails favoritos de um usuário
        /// </summary>
        /// <param name="email">Email a ser procurado as mensagens favoritas.</param>
        /// <returns>Lista de messages</returns>
        [HttpGet("buscaFavoritos/{email}")]
        public async Task<List<MessageMongoModel>>ListEmailsList(string email)
            => await _messageService.EmailFavoritelList(email);

        /// <summary>
        /// Lista possíveis spans de um usuário
        /// </summary>
        /// <param name="email">Email a ser procurado as spans.</param>
        /// <returns>Lista de messages</returns>
        [HttpGet("buscaSpan/{email}")]
        public async Task<List<MessageMongoModel>> ListEmailsSpam(string email)
            => await _messageService.GetSpans(email);

        /// <summary>
        /// Insere uma nova mensagem.
        /// </summary>
        /// <param name="message">A mensagem a ser inserida.</param>
        /// <returns>Status 200.</returns>
        [HttpPost("enviarEmail")]
        public async Task<IActionResult> sendoEmail([FromBody] MessageModel message)
        {
            await _messageService.InsertMessage(message);
            return Ok("Mensagem enviada com sucesso");
        }

        /// <summary>
        /// Favorita um email
        /// </summary>
        /// <param name="guidMessage">guid da mensagem.</param>
        /// <returns>Status 200.</returns>
        [HttpGet("favoriteMessage/{guidMessage}")]
        public async Task<IActionResult> UpdateMesssage(string guidMessage)
        {
            var valid = await _messageService.FavoriteMessage(guidMessage);
            if(valid)
            {
                return Ok("Mensagem favoritada com sucesso");
            }
            else
            {
                return BadRequest("Erro ao favoritar mensagem");
            }
        }

    }
}

