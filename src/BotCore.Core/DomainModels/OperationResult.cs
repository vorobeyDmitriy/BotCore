namespace BotCore.Core.DomainModels
{
    public class OperationResult
    {
        public OperationResult(string error)
        {
            Error = error;
        }

        public OperationResult()
        {
            Error = string.Empty;
        }

        public string Error { get; set; }
        public bool Success => string.IsNullOrWhiteSpace(Error);
    }
}