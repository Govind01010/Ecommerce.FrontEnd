using Discount.Domain.Shared;
using FrontEnd.Models.Dtos;
using FrontEnd.Services.IServices;
using FrontEnd.Utility;

namespace FrontEnd.Services
{
    public class DiscountCouponService : IDiscountCouponService
    {
        private readonly IBaseService _baseService;
        public DiscountCouponService(IBaseService baseService)
        {
            _baseService = baseService;
        }
        public async Task<Result> CreateAsync(DiscountDto coupon)
        {
            return await _baseService.SendAsync<Result>(new RequestDto
            {
                ApiType = SD.ApiType.POST,
                Url = SD.DiscountCouponApiBase + "/api/discount-coupon/create-coupon",
                Data = coupon
            });
        }

        public async Task<Result> DeleteAsync(int id)
        {
            return await _baseService.SendAsync<Result>(new RequestDto
            {
                ApiType = SD.ApiType.DELETE,
                Url = SD.DiscountCouponApiBase + "/api/discount-coupon/delete-coupon" + id
            });
        }

        public async Task<Result<Result<List<DiscountDto>>>?> GetAllAsync()
        {
            return await _baseService.SendAsync<Result<List<DiscountDto>>>(new RequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = SD.DiscountCouponApiBase + "/api/discount-coupon/get-all-coupons",
            });
        }

        public async Task<Result<DiscountDto>?> GetByIdAsync(int id)
        {
            return await _baseService.SendAsync<DiscountDto>(new RequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = SD.DiscountCouponApiBase + "/api/discount-coupon/get-coupon-by-id/" + id
            });
        }

        public async Task<Result<DiscountDto>?> UpdateAsync(DiscountDto coupon)
        {
            return await _baseService.SendAsync<DiscountDto>(new RequestDto
            {
                ApiType = SD.ApiType.PUT,
                Url = SD.DiscountCouponApiBase + "/api/discount-coupon/update-coupon",
                Data = coupon
            });
        }
    }
}
