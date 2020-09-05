using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Telegram.Bot.Types;

namespace TelegramBotCore.API.Controllers
{
    [ApiController]
    [Route("bot")]
    public class TelegramController : ControllerBase 
    {
        
        [HttpPost]
        public async Task Update([FromBody] Update telegramUpdate)
        {
            
        }
    }
}