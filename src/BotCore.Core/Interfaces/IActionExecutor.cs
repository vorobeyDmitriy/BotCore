using System.Threading.Tasks;
using BotCore.Core.DomainModels;

namespace BotCore.Core.Interfaces
{
    /// <summary>
    ///     Interface for automated mapping messenger commands to actions and execute it
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IActionExecutor<in T>
        where T : MessengerCommandBase
    {
        /// <summary>
        ///     Map <typeparamref name="T" /> to <see cref="IAction{T}" /> and  execute it
        /// </summary>
        /// <param name="messengerCommandBase">Command from messenger</param>
        /// <returns></returns>
        Task<OperationResult> ExecuteActionAsync(T messengerCommandBase);
    }
}