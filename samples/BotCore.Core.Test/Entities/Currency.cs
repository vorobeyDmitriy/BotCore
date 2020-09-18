using System.Collections.Generic;

namespace BotCore.Core.Test.Entities
{
    public class Currency : BaseEntity
    {
        public string Name { get; set; }
        public string Abbreviation { get; set; }
        public int Scale { get; set; }
        public decimal Rate { get; set; }

        public virtual ICollection<CurrencyRate> FromCurrencyRates { get; set; }
        public virtual ICollection<CurrencyRate> ToCurrencyRates { get; set; }
        public virtual ICollection<UserCurrencyMapping> UserCurrencyMapping { get; set; }
    }
}