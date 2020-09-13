using System.Threading.Tasks;

namespace BotCore.Core.Test.Interfaces
{
    public interface IMessageService
    {
        public Task<string> GetAllCurrenciesMessageAsync(int pageNumber, int pageSize);
    }
}