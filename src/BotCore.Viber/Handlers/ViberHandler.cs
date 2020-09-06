using System.Threading.Tasks;
using BotCore.Core.Interfaces;
using BotCore.Viber.DomainModels;
using BotCore.Viber.Interfaces;
using Viber.Bot;

namespace BotCore.Viber.Handlers
{
    public class ViberHandler : IViberHandler
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
            
            if(update.Message.Type != MessageType.Text)
                return;

            if(!(update.Message is TextMessage message))
                return;

            await _actionExecutor.ExecuteActionAsync(new ViberCommand(message.Text)
            {
                Receiver = update.Sender.Id
            });
        }
    }
}