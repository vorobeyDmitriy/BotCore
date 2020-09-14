using System.Threading.Tasks;

namespace BotCore.Core.Test.Interfaces
{
    public interface IMessageService
    {
        Task<string> GetAllCurrenciesMessageAsync(int pageNumber, int pageSize);
        Task<string> GetCurrencyRateMessageAsync(string currency);
        Task<int> GetCurrenciesCountAsync();
    }
}