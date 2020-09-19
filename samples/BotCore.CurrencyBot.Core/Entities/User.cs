using System.Collections.Generic;

namespace BotCore.Core.CurrencyBot.Entities
{
    public class User : BaseEntity
    {
        public long TelegramId { get; set; }
        public string Username { get; set; }

        public virtual ICollection<UserCurrencyMapping> UserCurrencyMapping { get; set; }
    }
}