using System.Threading.Tasks;

namespace BotCore.Core.Test.Interfaces
{
    public interface IApiProvider
    {
        Task<T> GetAsync<T>(string url) where T : class;
        Task<T> PostAsync<T, T1>(string url, T1 requestObject) where T : class;
    }
}