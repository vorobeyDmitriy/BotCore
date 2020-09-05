using System.Threading.Tasks;
using BotCore.Core.Interfaces;
using BotCore.Telegram.DomainModels;
using Microsoft.AspNetCore.Mvc;
using Telegram.Bot.Types;

namespace BotCore.API.Controllers
{
    [ApiController]
    [Route("bot")]
    public class TelegramController : ControllerBase
    {
        private readonly IActionExecutor _actionExecutor;

        public TelegramController(IActionExecutor actionExecutor)
        {
            _actionExecutor = actionExecutor;
        }

        [HttpPost]
        public async Task Update([FromBody] Update telegramUpdate)
        {
            var botCommand = new TelegramCommand(telegramUpdate.Message.Text);
            
            await _actionExecutor.ExecuteActionAsync(botCommand);
        }
    }
}