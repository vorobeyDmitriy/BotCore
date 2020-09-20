namespace BotCore.Core.DomainModels
{
    /// <summary>
    ///     Base class for all messenger commands
    /// </summary>
    public abstract class MessengerCommandBase
    {
        protected MessengerCommandBase(string commandName)
        {
            CommandName = commandName;
        }

        /// <summary>
        ///     Messenger command name
        /// </summary>
        public string CommandName { get; }
    }
}