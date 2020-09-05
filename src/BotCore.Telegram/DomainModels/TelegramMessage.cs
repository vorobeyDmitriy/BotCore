using BotCore.Core.DataTransfer;
using Telegram.Bot.Types.ReplyMarkups;

namespace BotCore.Telegram.DomainModels
{
    public class TelegramMessage : Message
    {
        public IReplyMarkup Keyboard { get; set; } 
    }
}