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
        [HttpGet("{email}", Name = "EmailListReceive") ]
        public async Task<List<MessageMongoModel>> EmailListReceive(string email)
            => await _messageService.EmailListReceive(email);

        /// <summary>
        /// Lista os emails enviados de um usuário
        /// </summary>
        /// <param name="email">Email a ser procurado as mensagens.</param>
        /// <returns>Status 200.</returns>
        [HttpGet("{email}", Name = "EmailListSend")]
        public async Task<List<MessageMongoModel>> EmailListSend(string email)
            => await _messageService.EmailListSend(email);

        /// <summary>
        /// Lista os emails favoritos de um usuário
        /// </summary>
        /// <param name="email">Email a ser procurado as mensagens favoritas.</param>
        /// <returns>Status 200.</returns>
        public async Task<List<MessageMongoModel>> EmailFavoritelList(string email)
            => await _messageService.EmailFavoritelList(email);
    }
}

