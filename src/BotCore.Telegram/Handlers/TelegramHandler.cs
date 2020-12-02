using System.Linq;
using System.Threading.Tasks;
using BotCore.Core;
using BotCore.Core.DomainModels;
using BotCore.Core.Interfaces;
using BotCore.Telegram.DomainModels;
using Telegram.Bot.Types;

namespace BotCore.Telegram.Handlers
{
    /// <inheritdoc cref="IHandler{T}" />
    public class TelegramHandler : IHandler<Update>
    {
        private readonly IActionExecutor<TelegramCommand> _actionExecutor;

        public TelegramHandler(IActionExecutor<TelegramCommand> actionExecutor)
        {
            _actionExecutor = actionExecutor;
        }

        public async Task<OperationResult> HandleUpdateAsync(Update telegramUpdate)
        {
            if (telegramUpdate?.Message?.Chat == null)
                return new OperationResult(Constants.IncomingMessageIsNull);

            var command = GetTelegramCommand(telegramUpdate);
            
            return await _actionExecutor.ExecuteActionAsync(command);
        }

        private static TelegramCommand GetTelegramCommand(Update telegramUpdate)
        {
            string commandName = null;

            if (telegramUpdate.Message.ReplyToMessage != null)
                commandName = telegramUpdate.Message.ReplyToMessage.Text.Split("\n").FirstOrDefault();

            if (string.IsNullOrWhiteSpace(commandName))
                commandName = telegramUpdate.Message.Text ?? string.Empty;

            commandName = commandName.Replace(" ", string.Empty);

            if (commandName.StartsWith('/'))
                commandName = commandName.Substring(1);

            var command = CreateCommand(commandName, telegramUpdate);

            return command;
        }

        private static TelegramCommand CreateCommand(string commandName, Update update)
        {
            var command = new TelegramCommand(commandName);
            var text = update.Message.Text ?? command.CommandName;
            command.ChatId = update.Message.Chat.Id;
            command.Text = text;
            command.MessageId = update.Message.MessageId;
            command.SenderUsername = update.Message.From.Username;
            command.UserId = update.Message.From.Id;

            return command;
        }
    }
}