using Discount.Domain.Shared;
using FrontEnd.Models.Dtos;
using FrontEnd.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FrontEnd.Controllers
{
    public class DiscountCouponController : Controller
    {
        private readonly IDiscountCouponService _discountCouponService;
        public DiscountCouponController(IDiscountCouponService discountCouponService)
        {
            _discountCouponService = discountCouponService;
        }
        public async Task<IActionResult> Index()
        {
            List<DiscountDto> discoutCouponsList = new(); 
           Result<Result<List<DiscountDto>>>? result = await _discountCouponService.GetAllAsync();

            if (result != null && result.IsSuccess && result._value != null)
            {
                // Serialize the object to JSON and then deserialize it properly
                string json = JsonConvert.SerializeObject(result._value);
                discoutCouponsList = JsonConvert.DeserializeObject<List<DiscountDto>>(Convert.ToString(json));
            }

            return View(discoutCouponsList);
        }
    }
}
