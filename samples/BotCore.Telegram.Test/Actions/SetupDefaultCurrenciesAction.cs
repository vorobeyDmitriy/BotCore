using System.Threading.Tasks;
using BotCore.Core.Interfaces;
using BotCore.Core.Test.Constants;
using BotCore.Core.Test.Interfaces;
using BotCore.Telegram.DataTransfer;
using BotCore.Telegram.DomainModels;
using BotCore.Telegram.Test.Keyboards;
using Telegram.Bot.Types.ReplyMarkups;

namespace BotCore.Telegram.Test.Actions
{
    public class SetupDefaultCurrenciesAction : TelegramAction
    {
        private readonly IUsersService _usersService;

        public SetupDefaultCurrenciesAction(IMessageSender<TelegramMessage> messageSender, IUsersService usersService)
            : base(messageSender)
        {
            _usersService = usersService;
        }

        public override async Task ExecuteAsync(TelegramCommand commandBase)
        {
            if (IsSetupMessage(commandBase.Text))
            {
                await MessageSender.SendTextAsync(
                    new TelegramMessage
                    {
                        Keyboard = new ForceReplyMarkup(),
                        Text = $"{ActionConstants.SetupDefaultCurrencies} \r\n " +
                               $"{MessagesConstants.SetupDefaultCurrencies}",
                        Receiver = commandBase.ChatId.ToString(),
                        ReplyToMessageId = commandBase.MessageId
                    });
            }
            else
            {
                await _usersService.SetUserDefaultCurrencies(commandBase.SenderUsername, commandBase.Text);
                await MessageSender.SendTextAsync(
                    new TelegramMessage
                    {
                        Keyboard = GetCurrencyRateKeyboard.Keyboard,
                        Text = MessagesConstants.SetupDefaultCurrenciesSuccess,
                        Receiver = commandBase.ChatId.ToString()
                    });
            }
        }

        private static bool IsSetupMessage(string text)
        {
            return text.Equals(ActionConstants.SetupDefaultCurrencies);
        }
    }
}