using System.Threading.Tasks;
using BotCore.Core.DomainModels;

namespace BotCore.Core.Interfaces
{
    public interface IHandler<in T>
    {
        /// <summary>
        ///     Method to process information (type of <typeparamref name="T" />) from messenger
        /// </summary>
        /// <param name="update">Update from messenger</param>
        /// <returns></returns>
        Task<OperationResult> HandleUpdateAsync(T update);
    }
}