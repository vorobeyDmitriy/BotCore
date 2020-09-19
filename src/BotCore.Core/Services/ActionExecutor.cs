using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotCore.Core.DataTransfer;
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

        public async Task<OperationResult> ExecuteActionAsync(T messengerCommandBase)
        {
            var command = GetAction(messengerCommandBase.CommandName);

            if (command != null)
                return await command.ExecuteAsync(messengerCommandBase);
            
            return new OperationResult(Constants.CommandNotFound);
        }

        private IAction<T> GetAction(string commandName)
        {
            return _commands.FirstOrDefault(x => x.Name.Equals(commandName,
                StringComparison.InvariantCultureIgnoreCase));
        }
    }
}