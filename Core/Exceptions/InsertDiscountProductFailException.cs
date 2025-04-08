namespace Core.Exceptions
{
    [Serializable]
    public class InsertDiscountProductFailException : Exception
    {
        public List<string> Errors { get; set; }
        public InsertDiscountProductFailException(string message, List<string> errors) : base(message)

        {
            Errors = errors;
        }
    }
}
