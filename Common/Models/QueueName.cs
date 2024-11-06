namespace CommonLibrary.Models
{
    public static class QueueName
    {
        public const string CommandQueue = "Update";
        public const string StatusQueue = "Status";
    }
    public enum WriteStatus
    {
        CreateSuccessful, 
        CreateFailed,
        UpdateSuccessFul,
        UpdateFailed,
    }
}
