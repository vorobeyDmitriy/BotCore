using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotCore.Core.DomainModels;
using BotCore.Core.Interfaces;

namespace BotCore.Core.Services
{
    /// <inheritdoc cref="IActionExecutor{T}" />
    /// <typeparam name="T"></typeparam>
    public class ActionExecutor<T> : IActionExecutor<T>
        where T : MessengerCommandBase
    {
        private readonly IEnumerable<IAction<T>> _commands;

        public ActionExecutor(IEnumerable<IAction<T>> commands)
        {
            _commands = commands;
        }

        public async Task ExecuteActionAsync(T messengerCommandBase)
        {
            var command = GetAction(messengerCommandBase.CommandName);

            if (command != null)
                await command.ExecuteAsync(messengerCommandBase);
        }

        private IAction<T> GetAction(string commandName)
        {
            return _commands.FirstOrDefault(x => x.Name.Equals(commandName,
                StringComparison.InvariantCultureIgnoreCase));
        }
    }
}