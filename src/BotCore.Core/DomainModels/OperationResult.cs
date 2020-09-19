namespace BotCore.Core.DomainModels
{
    public class OperationResult
    {
        public string Error { get; set; }
        public bool Success => string.IsNullOrWhiteSpace(Error);

        public OperationResult(string error)
        {
            Error = error;
        }

        public OperationResult()
        {
        }
    }
}