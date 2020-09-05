using System.Threading.Tasks;
using TelegramBotCore.Core.Interfaces;

namespace TelegramBotCore.Core.DataTransfer
{
    public abstract class ActionBase : IAction
    {
        private const string Action = "Action";
        public string Name => GetType().Name.Replace(Action, string.Empty);
        
        public abstract Task ExecuteAsync();
    }
}