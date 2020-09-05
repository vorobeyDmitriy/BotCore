using System.Threading.Tasks;

namespace TelegramBotCore.Core.Interfaces
{
    public interface ICommand
    {
        string Name { get; }
        Task ExecuteAsync();
    }
}