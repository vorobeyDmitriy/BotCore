using System.Threading.Tasks;
using BotCore.Core.DataTransfer;

namespace BotCore.Core.Interfaces
{
    public interface IMessageSender<T>
        where T : MessageBase
    {
        Task SendTextAsync(T message);
    }
}