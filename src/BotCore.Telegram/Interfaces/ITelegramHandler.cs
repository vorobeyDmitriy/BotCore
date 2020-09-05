using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace BotCore.Telegram.Interfaces
{
    public interface ITelegramHandler
    {
        Task HandleUpdate(Update telegramUpdate);
    }
}