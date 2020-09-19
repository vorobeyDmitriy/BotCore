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

        public async Task<OperationResult> HandleUpdate(Update telegramUpdate)
        {
            if (telegramUpdate?.Message?.Chat == null)
                return new OperationResult(Constants.IncomingMessageIsNull);

            var command = GetTelegramCommand(telegramUpdate.Message);

            var text = telegramUpdate.Message.Text ?? command.CommandName;

            command.ChatId = telegramUpdate.Message.Chat.Id;
            command.Text = text;
            command.MessageId = telegramUpdate.Message.MessageId;
            command.SenderUsername = telegramUpdate.Message.From.Username;
            command.UserId = telegramUpdate.Message.From.Id;

            return await _actionExecutor.ExecuteActionAsync(command);
        }

        private static TelegramCommand GetTelegramCommand(Message message)
        {
            string commandName = null;

            if (message.ReplyToMessage != null)
                commandName = message.ReplyToMessage.Text.Split("\n").FirstOrDefault();

            if (string.IsNullOrWhiteSpace(commandName))
                commandName = message.Text ?? string.Empty;

            commandName = commandName.Replace(" ", string.Empty);

            if (commandName.StartsWith('/'))
                commandName = commandName.Substring(1);

            return new TelegramCommand(commandName);
        }
    }
}