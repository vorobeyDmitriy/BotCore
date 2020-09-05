using System.Threading.Tasks;
using TelegramBotCore.Core.Interfaces;

namespace TelegramBotCore.Core.DataTransfer
{
    public abstract class CommandBase : ICommand
    {
        private const string Command = "Command";
        public string Name => GetType().Name.Replace(Command, string.Empty);
        
        public abstract Task ExecuteAsync();
    }
}