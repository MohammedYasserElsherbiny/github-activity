
namespace github_activity
{
    [Serializable]
    internal class jsonException : Exception
    {
        public jsonException()
        {
        }

        public jsonException(string? message) : base(message)
        {
        }

        public jsonException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}