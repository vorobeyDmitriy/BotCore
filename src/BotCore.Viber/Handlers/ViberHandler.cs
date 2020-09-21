using System.Threading.Tasks;
using BotCore.Core;
using BotCore.Core.DomainModels;
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

        public async Task<OperationResult> HandleUpdateAsync(CallbackData update)
        {
            if (update?.Event != EventType.Message || update.Message?.Type != MessageType.Text || update.Sender == null)
                return new OperationResult(Constants.IncomingMessageIsNull);

            if (!(update.Message is TextMessage message))
                return new OperationResult(Constants.IncomingMessageIsNull);

            return await _actionExecutor.ExecuteActionAsync(
                new ViberCommand(message.Text.Replace(" ", string.Empty))
                {
                    Receiver = update.Sender.Id
                });
        }
    }
}