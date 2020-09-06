using System.Threading.Tasks;
using BotCore.Core.Interfaces;
using Telegram.Bot.Types;

namespace BotCore.Telegram.Interfaces
{
    public interface ITelegramHandler : IHandler<Update>
    {
        // Task HandleUpdate(Update telegramUpdate);
    }
}