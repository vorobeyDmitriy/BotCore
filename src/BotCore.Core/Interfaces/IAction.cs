using System.Threading.Tasks;

namespace BotCore.Core.Interfaces
{
    public interface IAction
    {
        string Name { get; }
        Task ExecuteAsync();
    }
}