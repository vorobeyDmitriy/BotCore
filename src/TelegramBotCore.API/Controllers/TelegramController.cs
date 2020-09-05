using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Telegram.Bot.Types;
using TelegramBotCore.Core.DomainModels.MessengerCommands;
using TelegramBotCore.Core.Interfaces;

namespace TelegramBotCore.API.Controllers
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