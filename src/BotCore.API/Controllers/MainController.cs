using BotCore.Core.Interfaces;
using BotCore.Telegram.Controllers;

namespace BotCore.API.Controllers
{
    public class MainController : TelegramController
    {
        public MainController(IActionExecutor actionExecutor) : base(actionExecutor)
        {
        }
    }
}