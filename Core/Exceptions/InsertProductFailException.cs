namespace Core.Exceptions
{
    [Serializable]
    public class InsertProductFailException : Exception
    {
        public List<string> Errors { get; set; }
        public InsertProductFailException(string message, List<string> errors) : base(message)
        {
            Errors = errors;
        }
    }
}
