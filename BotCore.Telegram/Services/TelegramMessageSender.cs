using System.Threading.Tasks;
using Telegram.Bot;
using TelegramBotCore.Core.DataTransfer;
using TelegramBotCore.Core.Interfaces;

namespace BotCore.Telegram.Services
{
    public class TelegramMessageSender : IMessageSender
    {
        private readonly ITelegramBotClient _telegram;

        public TelegramMessageSender(ITelegramBotClient telegram)
        {
            _telegram = telegram;
        }

        public async Task SendTextAsync(Message message)
        {
            await _telegram.SendTextMessageAsync(message.Receiver, message.Text);
        }
    }
}