using System.Threading.Tasks;
using BotCore.Core.Interfaces;
using BotCore.Core.Test.Interfaces;
using BotCore.Viber.DataTransfer;
using BotCore.Viber.DomainModels;
using BotCore.Viber.Test.Keyboards;

namespace BotCore.Viber.Test.Actions
{
    public class GetAllCurrenciesAction : ViberAction
    {
        private const int PageSize = 8;
        private readonly IMessageService _messageService;

        public GetAllCurrenciesAction(IMessageSender<ViberMessage> messageSender, IMessageService messageService)
            : base(messageSender)
        {
            _messageService = messageService;
        }

        public override async Task ExecuteAsync(ViberCommand command)
        {
            var pageNumber = 0;
            var message = await _messageService.GetAllCurrenciesMessageAsync(pageNumber, PageSize);
            while (string.IsNullOrWhiteSpace(message))
            {
                await MessageSender.SendTextAsync(new ViberMessage
                {
                    Receiver = command.Receiver,
                    Keyboard = GetCurrencyRateKeyboard.Keyboard,
                    Text = message,
                    SenderDisplayName = "Qwe"
                });

                pageNumber++;
            }
        }
    }
}