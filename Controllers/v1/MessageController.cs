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
        /// Obtém a lista de mensagens.
        /// </summary>
        /// <returns>Lista de mensagens.</returns>
        [HttpGet(Name = "ListMessages")]
        public async Task<List<MessageMongoModel>> ListMessages()
            => await _messageService.ListMessages();

        /// <summary>
        /// Insere uma nova mensagem.
        /// </summary>
        /// <param name="message">A mensagem a ser inserida.</param>
        /// <returns>Status 200.</returns>
        [HttpPost(Name = "InsertMessages")]
        public async Task<IActionResult> InsertMessage([FromBody] MessageModel message)
        {
            await _messageService.InsertMessage(message);
            return Ok("Mensagem enviada com sucesso");
        }

        /// <summary>
        /// Lista os emails recebidos de um usuário
        /// </summary>
        /// <param name="email">Email a ser procurado as mensagens.</param>
        /// <returns>Status 200.</returns>
        [HttpGet("receive/{email}", Name = "EmailListReceive")]
        public async Task<ActionResult<List<MessageMongoModel>>> EmailListReceive(string email)
        {
            var result = await _messageService.EmailListReceive(email);
            if (result == null)
            {
                return BadRequest("Invalid email address");
            }
            return Ok(result);
        }

        /// <summary>
        /// Lista os emails enviados de um usuário
        /// </summary>
        /// <param name="email">Email a ser procurado as mensagens.</param>
        /// <returns>Status 200.</returns>
        [HttpGet("send/{email}", Name = "EmailListSend")]
        public async Task<ActionResult<List<MessageMongoModel>>> EmailListSend(string email)
        {
            var result = await _messageService.EmailListSend(email);
            if (result == null)
            {
                return BadRequest("Invalid email address");
            }
            return Ok(result);
        }

        /// <summary>
        /// Lista os emails favoritos de um usuário
        /// </summary>
        /// <param name="email">Email a ser procurado as mensagens favoritas.</param>
        /// <returns>Lista de messages</returns>
        [HttpGet("{email}", Name = "EmailFavoritelList")]
        public async Task<List<MessageMongoModel>> EmailFavoritelList(string email)
            => await _messageService.EmailFavoritelList(email);

        /// <summary>
        /// Lista possíveis spans de um usuário
        /// </summary>
        /// <param name="email">Email a ser procurado as spans.</param>
        /// <returns>Lista de messages</returns>
        [HttpGet("spam/{email}", Name = "GetSpans")]
        public async Task<List<MessageMongoModel>> GetSpans(string email)
            => await _messageService.GetSpans(email);
    }
}

