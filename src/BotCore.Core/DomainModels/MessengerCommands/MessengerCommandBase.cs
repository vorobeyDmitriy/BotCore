namespace TelegramBotCore.Core.DomainModels.MessengerCommands
{
    public abstract class MessengerCommandBase
    {
        public string CommandName { get; set; }

        protected MessengerCommandBase(string commandName)
        {
            CommandName = CommandName;
        }
    }
}