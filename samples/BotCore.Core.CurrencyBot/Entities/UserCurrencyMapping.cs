namespace BotCore.Core.CurrencyBot.Entities
{
    public class UserCurrencyMapping : BaseEntity
    {
        public int UserId { get; set; }
        public int CurrencyId { get; set; }

        public virtual User User { get; set; }
        public virtual Currency Currency { get; set; }
    }
}