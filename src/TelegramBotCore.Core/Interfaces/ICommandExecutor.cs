using System.Threading.Tasks;
using TelegramBotCore.Core.DomainModels;

namespace TelegramBotCore.Core.Interfaces
{
    public interface ICommandExecutor
    {
        Task ExecuteCommand(MessengerCommand messengerCommand);
    }
}