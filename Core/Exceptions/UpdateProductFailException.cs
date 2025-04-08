namespace Core.Exceptions
{
    [Serializable]
    public class UpdateProductFailException : Exception
    {
        public List<string> Errors;
        public UpdateProductFailException(string message, List<string> error) : base(message)
        {
            Errors = error;
        }
    }
}
