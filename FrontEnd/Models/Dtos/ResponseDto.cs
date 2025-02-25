namespace FrontEnd.Models.Dtos
{
    public class ResponseDto<T>
    {
        public T? Result { get; set; }
        public string Message { get; set; } = "";
        public bool IsSuccess { get; set; } = true;
    }
}
