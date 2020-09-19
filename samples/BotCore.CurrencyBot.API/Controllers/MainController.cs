using System.Threading.Tasks;
using BotCore.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Telegram.Bot.Types;
using Viber.Bot;

namespace BotCore.CurrencyBot.API.Controllers
{
    [ApiController]
    [Route("bot")]
    public class MainController : Controller
    {
        private readonly IHandler<Update> _telegramHandler;
        private readonly IHandler<CallbackData> _viberHandler;

        public MainController(IHandler<Update> telegramHandler, IHandler<CallbackData> viberHandler)
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