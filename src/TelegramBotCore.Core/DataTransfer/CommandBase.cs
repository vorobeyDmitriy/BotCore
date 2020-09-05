using System.Threading.Tasks;
using TelegramBotCore.Core.Interfaces;

namespace TelegramBotCore.Core.DataTransfer
{
    public abstract class CommandBase : ICommand
    {
        public string Name => GetType().Name;
        
        public abstract Task ExecuteAsync();
    }
}