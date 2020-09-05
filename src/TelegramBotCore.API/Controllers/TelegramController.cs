using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Telegram.Bot.Types;
using TelegramBotCore.Core.DomainModels;
using TelegramBotCore.Core.Interfaces;

namespace TelegramBotCore.API.Controllers
{
    [ApiController]
    [Route("bot")]
    public class TelegramController : ControllerBase
    {
        private readonly ICommandExecutor _commandExecutor;

        public TelegramController(ICommandExecutor commandExecutor)
        {
            _commandExecutor = commandExecutor;
        }

        [HttpPost]
        public async Task Update([FromBody] Update telegramUpdate)
        {
            var botCommand = new MessengerCommand
            {
                CommandName = telegramUpdate.Message.Text
            };
            
            await _commandExecutor.ExecuteCommand(botCommand);
        }
    }
}