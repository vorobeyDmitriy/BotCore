using System.Threading.Tasks;
using TelegramBotCore.Core.DomainModels;

namespace TelegramBotCore.Core.Interfaces
{
    public interface IActionExecutor
    {
        Task ExecuteActionAsync(MessengerCommandBase messengerCommandBase);
    }
}