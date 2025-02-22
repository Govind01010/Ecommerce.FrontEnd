using FrontEnd.Models.Dtos;

namespace FrontEnd.Services.IServices
{
    public interface IBaseService
    {
        Task<ResponseDto?> SendAsync(RequestDto requestDto);
    }
}
