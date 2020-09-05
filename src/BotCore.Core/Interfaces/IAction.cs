using System.Threading.Tasks;
using BotCore.Core.DomainModels;

namespace BotCore.Core.Interfaces
{
    public interface IAction
    {
        string Name { get; }
        Task ExecuteAsync(MessengerCommandBase commandBase);
    }
}