using Challenge_Locaweb.Models;
using Challenge_Locaweb.Services;
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
        [HttpGet("bin/{email}")]
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
        [HttpGet("receive/{email}")]
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
        [HttpGet("send/{email}")]
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
        [HttpGet("{email}")]
        public async Task<List<MessageMongoModel>>ListEmailsList(string email)
            => await _messageService.EmailFavoritelList(email);

        /// <summary>
        /// Lista possíveis spans de um usuário
        /// </summary>
        /// <param name="email">Email a ser procurado as spans.</param>
        /// <returns>Lista de messages</returns>
        [HttpGet("spam/{email}")]
        public async Task<List<MessageMongoModel>> ListEmailsSpam(string email)
            => await _messageService.GetSpans(email);

        /// <summary>
        /// Insere uma nova mensagem.
        /// </summary>
        /// <param name="message">A mensagem a ser inserida.</param>
        /// <returns>Status 200.</returns>
        [HttpPost]
        public async Task<IActionResult> sendoEmail([FromBody] MessageModel message)
        {
            await _messageService.InsertMessage(message);
            return Ok("Mensagem enviada com sucesso");
        }

    }
}

