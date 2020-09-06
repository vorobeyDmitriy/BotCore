using System.Threading.Tasks;
using BotCore.Telegram.Interfaces;
using BotCore.Viber.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Telegram.Bot.Types;
using Viber.Bot;

namespace BotCore.API.Controllers
{
    [ApiController]
    [Route("bot")]
    public class MainController : Controller
    {
        private readonly ITelegramHandler _telegramHandler;
        private readonly IViberHandler _viberHandler;

        public MainController(ITelegramHandler telegramHandler, IViberHandler viberHandler)
        {
            _telegramHandler = telegramHandler;
            _viberHandler = viberHandler;
        }

        [HttpPost("telegram")]
        public async Task<IActionResult> Update([FromBody] Update telegramUpdate)
        {
            await _telegramHandler.HandleUpdate(telegramUpdate);
            return Ok();
        }
        
        [HttpPost("viber")]
        public async Task<IActionResult> Update([FromBody] CallbackData viberUpdate)
        {
            await _viberHandler.HandleUpdate(viberUpdate);
            return Ok();
        }
    }
}