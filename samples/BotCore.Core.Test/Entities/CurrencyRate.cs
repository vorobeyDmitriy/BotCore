using System;

namespace BotCore.Core.Test.Entities
{
    public class CurrencyRate : BaseEntity
    {
        public int FromId { get; set; }
        public int ToId { get; set; }
        public double Rate { get; set; }
        public DateTime Date { get; set; }

        public virtual Currency From { get; set; }
        public virtual Currency To { get; set; }
    }
}