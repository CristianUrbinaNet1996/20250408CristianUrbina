namespace Core.Exceptions
{
    [Serializable]
    public class UpdateDiscountProductFailException : Exception
    {
        public List<string> Errors { get; set; }
        public UpdateDiscountProductFailException(string message, List<string> errors) : base(message)

        {
            Errors = errors;
        }

    }
}
