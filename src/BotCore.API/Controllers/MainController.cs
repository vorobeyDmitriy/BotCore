using System.Threading.Tasks;
using BotCore.Telegram.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Telegram.Bot.Types;

namespace BotCore.API.Controllers
{
    [ApiController]
    [Route("bot")]
    public class MainController : Controller
    {
        private readonly ITelegramHandler _telegramHandler;
        public MainController(ITelegramHandler telegramHandler) 
        {
            _telegramHandler = telegramHandler;
        }

        [HttpPost]
        public async Task<IActionResult> Update([FromBody] Update telegramUpdate)
        {
            await _telegramHandler.HandleUpdate(telegramUpdate);
            return Ok();
        }
    }
}