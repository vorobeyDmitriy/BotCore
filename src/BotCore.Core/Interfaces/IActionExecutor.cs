using System.Threading.Tasks;
using BotCore.Core.DomainModels;

namespace BotCore.Core.Interfaces
{
    public interface IActionExecutor
    {
        Task ExecuteActionAsync(MessengerCommandBase messengerCommandBase);
    }
}