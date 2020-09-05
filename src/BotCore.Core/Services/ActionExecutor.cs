using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotCore.Core.DomainModels;
using BotCore.Core.Interfaces;

namespace BotCore.Core.Services
{
    public class ActionExecutor : IActionExecutor
    {
        private readonly IEnumerable<IAction> _commands;

        public ActionExecutor(IEnumerable<IAction> commands)
        {
            _commands = commands;
        }

        public async Task ExecuteActionAsync(MessengerCommandBase messengerCommandBase)
        {
            var command = GetAction(messengerCommandBase.CommandName);
            await command.ExecuteAsync();
        }

        private IAction GetAction(string commandName)
        {
            return _commands.FirstOrDefault(x => x.Name.Equals(commandName,
                StringComparison.InvariantCultureIgnoreCase));
        }
    }
}