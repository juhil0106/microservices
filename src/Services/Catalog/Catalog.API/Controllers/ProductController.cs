using Catalog.API.Entities;
using Catalog.API.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _productRepository.GetProducts();
            return Ok(products);
        }

        [HttpGet]
        public async Task<IActionResult> GetProductById(string id)
        {
            var products = await _productRepository.GetProductsById(id);
            return Ok(products);
        }

        [HttpGet]
        public async Task<IActionResult> GetProductByName(string name)
        {
            var products = await _productRepository.GetProductsByName(name);
            return Ok(products);
        }

        [HttpGet]
        public async Task<IActionResult> GetProductByCategoryName(string categoryName)
        {
            var products = await _productRepository.GetProductsByCategoryName(categoryName);
            return Ok(products);
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(Product product)
        {
            var addProduct = await _productRepository.AddProducts(product);
            return addProduct > 0 ? Ok("Product added successfully.") : BadRequest("Unable to add product.");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProducts(Product product)
        {
            var updateProduct = await _productRepository.UpdateProducts(product);
            return updateProduct is true ? Ok("Product updated successfully.") : BadRequest("Unable to update product.");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteProduct(string id)
        {
            var deleteProduct = await _productRepository.DeleteProduct(id);
            return deleteProduct is true ? Ok("Product deleted successfully.") : BadRequest("Unable to delete product.");
        }
    }
}
