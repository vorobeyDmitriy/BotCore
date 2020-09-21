using System;
using BotCore.Core.CurrencyBot.Entities;

namespace BotCore.Core.CurrencyBot.Specifications
{
    public class CurrencyRateSpecification : BaseSpecification<CurrencyRate>
    {
        public CurrencyRateSpecification(string currencyName, DateTime dateTime)
            : base(x => x.Date == dateTime &&
                        x.From.Abbreviation == currencyName)
        {
            AddInclude(x => x.From);
        }
    }
}