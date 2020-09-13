using System.Threading.Tasks;
using BotCore.Core.DataTransfer;

namespace BotCore.Core.Interfaces
{
    public interface IMessageSender<in T>
        where T : MessageBase
    {
        /// <summary>
        ///     Send text message to user
        /// </summary>
        /// <param name="message">Message</param>
        /// <returns></returns>
        Task SendTextAsync(T message);
    }
}