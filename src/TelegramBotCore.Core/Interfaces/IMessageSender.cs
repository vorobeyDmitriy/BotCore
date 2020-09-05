using System.Threading.Tasks;

namespace TelegramBotCore.Core.Interfaces
{
    public interface IMessageSender
    {
        Task SendTextAsync();
        Task SendPictureAsync();
    }
}