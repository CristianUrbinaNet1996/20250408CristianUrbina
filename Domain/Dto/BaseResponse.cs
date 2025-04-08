namespace Domain.DTO
{
    public class BaseResponse<T>
    {

        public T? Data { get; set; }
        public bool Status { get; set; }

        public string? Message { get; set; }

        public List<string> Errors { get; set; }

        public BaseResponse()
        {

        }
        public BaseResponse(T? result)
        {
            Data = result;
            Message = "Ok";
            Status = true;
            Errors = new List<string>();
        }
    }
}
