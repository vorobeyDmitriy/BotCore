using System.Threading.Tasks;
using BotCore.Core.DataTransfer;
using BotCore.Core.DomainModels;

namespace BotCore.Core.Interfaces
{
    /// <summary>
    ///     Base interface for any actions
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IAction<in T>
        where T : MessengerCommandBase
    {
        /// <summary>
        ///     Action name
        /// </summary>
        string Name { get; }

        /// <summary>
        ///     Method for execute commands
        /// </summary>
        /// <param name="commandBase"><see cref="MessengerCommandBase" /> instance for executing</param>
        /// <returns></returns>
        Task<OperationResult> ExecuteAsync(T commandBase);
    }
}