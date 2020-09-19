using System.Threading.Tasks;
using BotCore.Core.DataTransfer;
using BotCore.Core.DomainModels;

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
        Task<OperationResult> SendTextAsync(T message);
    }
}