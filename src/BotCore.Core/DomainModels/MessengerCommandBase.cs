namespace BotCore.Core.DomainModels
{
    public abstract class MessengerCommandBase
    {
        protected MessengerCommandBase(string commandName)
        {
            CommandName = commandName;
        }

        public string CommandName { get; set; }
    }
}