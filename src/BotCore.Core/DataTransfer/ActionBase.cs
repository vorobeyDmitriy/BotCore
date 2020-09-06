using System.Threading.Tasks;
using BotCore.Core.DomainModels;
using BotCore.Core.Interfaces;

namespace BotCore.Core.DataTransfer
{
    public abstract class ActionBase<T> : IAction<T>
        where T : MessengerCommandBase
    {
        private const string Action = "Action";
        public string Name => GetType().Name.Replace(Action, string.Empty);

        public abstract Task ExecuteAsync(T commandBase);
    }
}