using System.Threading.Tasks;
using BotCore.Core.Interfaces;
using BotCore.Viber.DomainModels;
using Viber.Bot;

namespace BotCore.Viber.Handlers
{
    /// <inheritdoc cref="IHandler{T}" />
    public class ViberHandler : IHandler<CallbackData>
    {
        private readonly IActionExecutor<ViberCommand> _actionExecutor;

        public ViberHandler(IActionExecutor<ViberCommand> actionExecutor)
        {
            _actionExecutor = actionExecutor;
        }

        public async Task HandleUpdate(CallbackData update)
        {
            if (update.Event != EventType.Message)
                return;

            if (update.Message.Type != MessageType.Text)
                return;

            if (!(update.Message is TextMessage message))
                return;

            await _actionExecutor.ExecuteActionAsync(
                new ViberCommand(message.Text.Replace(" ", string.Empty))
                {
                    Receiver = update.Sender.Id
                });
        }
    }
}