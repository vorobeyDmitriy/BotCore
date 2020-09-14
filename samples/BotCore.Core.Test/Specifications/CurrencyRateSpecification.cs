using System;
using BotCore.Core.Test.Entities;

namespace BotCore.Core.Test.Specifications
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