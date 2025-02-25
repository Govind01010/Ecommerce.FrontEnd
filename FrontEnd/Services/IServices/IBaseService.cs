using Discount.Domain.Shared;
using FrontEnd.Models.Dtos;

namespace FrontEnd.Services.IServices
{
    public interface IBaseService
    {
        Task<Result<T>?> SendAsync<T>(RequestDto requestDto);
    }
}
