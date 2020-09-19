using System.Threading.Tasks;
using BotCore.Core.CurrencyBot.Constants;
using BotCore.Core.CurrencyBot.Entities;
using BotCore.Core.CurrencyBot.Interfaces;
using BotCore.Core.DataTransfer;
using BotCore.Core.DomainModels;
using BotCore.Core.Interfaces;
using BotCore.Telegram.CurrencyBot.Keyboards;
using BotCore.Telegram.DataTransfer;
using BotCore.Telegram.DomainModels;

namespace BotCore.Telegram.CurrencyBot.Actions
{
    public class StartAction : TelegramAction
    {
        private readonly IUsersService _usersService;

        public StartAction(IMessageSender<TelegramMessage> messageSender, IUsersService usersService)
            : base(messageSender)
        {
            _usersService = usersService;
        }

        public override async Task<OperationResult> ExecuteAsync(TelegramCommand command)
        {
            var user = await _usersService.GetUserByUsernameAsync(command.SenderUsername);

            if (user == null)
                user = await _usersService.CreateUserAsync(new User
                {
                    TelegramId = command.UserId,
                    Username = command.SenderUsername
                });

            await MessageSender.SendTextAsync(new TelegramMessage
            {
                Receiver = command.ChatId.ToString(),
                Keyboard = GetCurrencyRateKeyboard.Keyboard,
                Text = $"{MessagesConstants.Hello} {user.Username}"
            });
        }
    }
}