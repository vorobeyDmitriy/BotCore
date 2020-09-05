using System.Threading.Tasks;

namespace TelegramBotCore.Core.Interfaces
{
    public interface IAction
    {
        string Name { get; }
        Task ExecuteAsync();
    }
}