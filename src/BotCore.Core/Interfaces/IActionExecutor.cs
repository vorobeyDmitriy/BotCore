using System.Threading.Tasks;
using TelegramBotCore.Core.DomainModels.MessengerCommands;

namespace TelegramBotCore.Core.Interfaces
{
    public interface IActionExecutor
    {
        Task ExecuteActionAsync(MessengerCommandBase messengerCommandBase);
    }
}