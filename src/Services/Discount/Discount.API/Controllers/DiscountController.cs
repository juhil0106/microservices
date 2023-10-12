using Discount.API.Entities;
using Discount.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Discount.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DiscountController : ControllerBase
    {
        private readonly IDiscountRepository _discountRepository;

        public DiscountController(IDiscountRepository discountRepository)
        {
            _discountRepository = discountRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetDiscount(string productName)
        {
            var discount = await _discountRepository.GetDiscount(productName);
            return Ok(discount);    
        }

        [HttpPost]
        public async Task<IActionResult> AddDiscount(Coupon coupon)
        {
            var discount = await _discountRepository.CreateCoupon(coupon);
            return discount is true ? Ok("Product discount added successfully.") : BadRequest("Unable to add product discount.");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateDiscount(Coupon coupon)
        {
            var discount = await _discountRepository.UpdateCoupon(coupon);
            return discount is true ? Ok("Product discount updated successfully.") : BadRequest("Unable to update product discount.");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteDiscount(string productName)
        {
            var discount = await _discountRepository.DeleteCoupon(productName);
            return discount is true ? Ok("Product discount deleted successfully.") : BadRequest("Unable to delete product discount.");
        }
    }
}
