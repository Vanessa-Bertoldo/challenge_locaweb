using Challenge_Locaweb.Models;
using Challenge_Locaweb.Services;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace Challenge_Locaweb.Controllers.v1
{
    [ApiController]
    [Route("[controller]")]
    public class MessagesController : ControllerBase
    {

        private readonly MessageService _messageService;

        public MessagesController(MessageService messageService)
        {
            _messageService = messageService;
        }

        [HttpGet(Name = "ListMessages")]
        public async Task<List<MessageModel>> ListMessages()
            => await _messageService.ListMessages();

        //[HttpPost(Name = "InsertMessages")]
        //public async Task<IActionResult> InsertMessage([FromBody] Dictionary<string, object> document)
        //{
        //    await _messageService.InsertMessage(document);
        //    return document;
        //}

    }
}

/*
 [HttpPost]
        public async Task<Produto> PostProduto(Produto produto)
        {
            await _produtoServices.CreateAsync(produto);

            return produto;
        }
 */