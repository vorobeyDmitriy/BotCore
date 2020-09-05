namespace BotCore.Core.DomainModels
{
    public abstract class MessengerCommandBase
    {
        public string CommandName { get; set; }
        
        protected MessengerCommandBase(string commandName)
        {
            CommandName = commandName;
        }
    }
}