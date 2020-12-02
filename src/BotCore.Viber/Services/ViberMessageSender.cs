using System;
using System.IO;
using System.Threading.Tasks;
using BotCore.Core;
using BotCore.Core.DomainModels;
using BotCore.Core.Interfaces;
using BotCore.Viber.DomainModels;
using Viber.Bot;

namespace BotCore.Viber.Services
{
    /// <inheritdoc cref="IMessageSender{T}" />
    public class ViberMessageSender : IMessageSender<ViberMessage>
    {
        private readonly IViberBotClient _viber;

        public ViberMessageSender(IViberBotClient viber)
        {
            _viber = viber;
        }

        public async Task<OperationResult> SendTextAsync(ViberMessage message)
        {
            if (message==null)
                throw new ArgumentNullException(nameof(ViberMessage));
            
            var result = await _viber.SendKeyboardMessageAsync(new KeyboardMessage
            {
                Receiver = message.Receiver,
                Text = message.Text,
                Sender = new UserBase
                {
                    Name = message.SenderDisplayName
                },
                Keyboard = message.Keyboard
            });

            return result == 0
                ? new OperationResult(Constants.Error)
                : new OperationResult();
        }

        public Task<OperationResult> SendPictureAsync(ViberMessage message, string picturePath)
        {
            throw new NotImplementedException();
        }
    }
}