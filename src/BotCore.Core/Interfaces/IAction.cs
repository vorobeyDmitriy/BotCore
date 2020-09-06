using System.Threading.Tasks;
using BotCore.Core.DomainModels;

namespace BotCore.Core.Interfaces
{
    public interface IAction<T>
        where T : MessengerCommandBase
    {
        string Name { get; }
        Task ExecuteAsync(T commandBase);
    }
}