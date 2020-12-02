using System.IO;
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

        /// <summary>
        ///     Send picture with caption to user
        /// </summary>
        /// <param name="message">Message</param>
        /// <param name="picturePath">Path to picture on physical device</param>
        /// <returns></returns>
        Task<OperationResult> SendPictureAsync(T message, string picturePath);
    }
}