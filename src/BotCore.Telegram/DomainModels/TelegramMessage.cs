using BotCore.Core.DataTransfer;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace BotCore.Telegram.DomainModels
{
    public class TelegramMessage : Message
    {
        public ParseMode ParseMode { get; set; }
        public bool DisableWebPagePreview { get; set; }
        public bool DisableNotification { get; set; }
        public int ReplyToMessageId { get; set; }
        public IReplyMarkup Keyboard { get; set; }
    }
}