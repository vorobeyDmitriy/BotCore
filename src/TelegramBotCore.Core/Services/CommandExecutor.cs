using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TelegramBotCore.Core.DomainModels;
using TelegramBotCore.Core.Interfaces;

namespace TelegramBotCore.Core.Services
{
    public class CommandExecutor : ICommandExecutor
    {
        private readonly IEnumerable<ICommand> _commands;

        public CommandExecutor(IEnumerable<ICommand> commands)
        {
            _commands = commands;
        }

        public async Task ExecuteCommand(MessengerCommand messengerCommand)
        {
            var command = GetCommand(messengerCommand.CommandName);
            await command.ExecuteAsync();
        }

        private ICommand GetCommand(string commandName)
        {
            return _commands.FirstOrDefault(x => x.Name.Equals(commandName,
                StringComparison.InvariantCultureIgnoreCase));
        }
    }
}