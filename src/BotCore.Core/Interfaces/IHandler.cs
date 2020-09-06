using System.Threading.Tasks;

namespace BotCore.Core.Interfaces
{
    public interface IHandler<T>
    {
        Task HandleUpdate(T update);
    }
}