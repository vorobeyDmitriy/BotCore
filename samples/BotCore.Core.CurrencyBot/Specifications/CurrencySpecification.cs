using System.Collections.Generic;
using System.Linq;
using BotCore.Core.CurrencyBot.Entities;

namespace BotCore.Core.CurrencyBot.Specifications
{
    public class CurrencySpecification : BaseSpecification<Currency>
    {
        public CurrencySpecification(int pageNumber, int pageSize)
        {
            ApplyPaging(pageNumber, pageSize);
        }

        public CurrencySpecification(string abbreviation)
            : base(x => x.Abbreviation == abbreviation)
        {
        }

        public CurrencySpecification(IEnumerable<string> abbreviations)
            : base(x => abbreviations.Contains(x.Abbreviation))
        {
        }
    }
}