using System.Threading.Tasks;
using TelegramBotCore.Core.DataTransfer;

namespace TelegramBotCore.Core.Interfaces
{
    public interface IMessageSender
    {
        Task SendTextAsync(Message message);
    }
}