using System.Threading.Tasks;
using BotCore.Core.DomainModels;
using BotCore.Core.Interfaces;

namespace BotCore.Core.DataTransfer
{
    /// <inheritdoc cref="IAction{T}" />
    /// <typeparam name="T"></typeparam>
    public abstract class ActionBase<T> : IAction<T>
        where T : MessengerCommandBase
    {
        private const string Action = "Action";
        public string Name => GetType().Name.Replace(Action, string.Empty);

        public abstract Task ExecuteAsync(T commandBase);
    }
}