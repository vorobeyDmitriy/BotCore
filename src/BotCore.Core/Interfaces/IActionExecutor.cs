using System.Threading.Tasks;
using BotCore.Core.DomainModels;

namespace BotCore.Core.Interfaces
{
    public interface IActionExecutor<T>
        where T : MessengerCommandBase
    {
        Task ExecuteActionAsync(T messengerCommandBase);
    }
}