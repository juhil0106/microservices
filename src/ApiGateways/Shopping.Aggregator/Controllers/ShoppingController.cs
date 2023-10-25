using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shopping.Aggregator.Models;
using Shopping.Aggregator.Services;

namespace Shopping.Aggregator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingController : ControllerBase
    {
        private readonly ICatalogService _catalogService;
        private readonly IBasketService _basketService;
        private readonly IOrderService _orderService;

        public ShoppingController(ICatalogService catalogService, IBasketService basketService, IOrderService orderService)
        {
            _catalogService = catalogService;
            _basketService = basketService;
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<IActionResult> GetShopping(string userName)
        {
            var basket = await _basketService.GetBasketByUserName(userName);

            foreach (var item in basket.Items)
            {
                var product = await _catalogService.GetCatalogById(item.ProductId);

                item.Category = product.Category;
                item.Description = product.Description;
                item.ProductName = product.Name;
                item.ImageFile = product.ImagePath;
            }

            var order = await _orderService.GetOrdersByUserName(userName);

            var shoppingModel = new ShoppingModel()
            {
                UserName = userName,
                BasketWithProducts = basket,
                Orders = order
            };

            return Ok(shoppingModel);
        }
    }
}
