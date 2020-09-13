using BotCore.Core.Test.Entities;

namespace BotCore.Core.Test.Specifications
{
    public class CurrencySpecification : BaseSpecification<Currency>
    {
        public CurrencySpecification(int pageNumber, int pageSize)
        {
            ApplyPaging(pageNumber, pageSize);
        }
    }
}