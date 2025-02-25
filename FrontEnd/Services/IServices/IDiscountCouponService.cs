using Discount.Domain.Shared;
using FrontEnd.Models.Dtos;

namespace FrontEnd.Services.IServices
{
    public interface IDiscountCouponService
    {
        Task<Result<DiscountDto>?> GetByIdAsync(int id);
        Task<Result<Result<List<DiscountDto>>>?> GetAllAsync();
        Task<Result> CreateAsync(DiscountDto coupon);
        Task<Result<DiscountDto>?> UpdateAsync(DiscountDto coupon);
        Task<Result> DeleteAsync(int id);
    }
}
