using System.Threading.Tasks;
using BotCore.Core.DataTransfer;

namespace BotCore.Core.Interfaces
{
    public interface IMessageSender
    {
        Task SendTextAsync(Message message);
    }
}