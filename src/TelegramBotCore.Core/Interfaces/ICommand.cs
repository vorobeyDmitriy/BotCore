using System.Threading.Tasks;

namespace TelegramBotCore.Core.Interfaces
{
    public interface ICommand
    {
        Task ExecuteAsync();
    }
}