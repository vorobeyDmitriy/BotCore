﻿using System.Threading.Tasks;
using BotCore.Core.Interfaces;
using BotCore.Telegram.DomainModels;
using BotCore.Telegram.Interfaces;
using Telegram.Bot.Types;

namespace BotCore.Telegram.Handlers
{
    public class TelegramHandler : ITelegramHandler
    {
        private readonly IActionExecutor<TelegramCommand> _actionExecutor;

        public TelegramHandler(IActionExecutor<TelegramCommand> actionExecutor)
        {
            _actionExecutor = actionExecutor;
        }

        public async Task HandleUpdate(Update telegramUpdate)
        {
            var commandName = telegramUpdate.Message.Text.Replace(" ", string.Empty);

            if (commandName.StartsWith('/'))
                commandName = commandName.Substring(1);

            var botCommand = new TelegramCommand(commandName)
            {
                SenderId = telegramUpdate.Message.Chat.Id
            };
            await _actionExecutor.ExecuteActionAsync(botCommand);
        }
    }
}