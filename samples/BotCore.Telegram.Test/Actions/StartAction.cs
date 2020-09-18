using System.Threading.Tasks;
using BotCore.Core.Interfaces;
using BotCore.Core.Test.Constants;
using BotCore.Core.Test.Entities;
using BotCore.Core.Test.Interfaces;
using BotCore.Telegram.DataTransfer;
using BotCore.Telegram.DomainModels;
using BotCore.Telegram.Test.Keyboards;

namespace BotCore.Telegram.Test.Actions
{
    public class StartAction : TelegramAction
    {
        private readonly IUsersService _usersService;

        public StartAction(IMessageSender<TelegramMessage> messageSender, IUsersService usersService)
            : base(messageSender)
        {
            _usersService = usersService;
        }

        public override async Task ExecuteAsync(TelegramCommand command)
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