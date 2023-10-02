using Catalog.API.Entities;

namespace Catalog.API.Repository
{
    public interface IProductRepository
    {
        Task<List<Product>> GetProducts();
        Task<Product> GetProductsById(string id);
        Task<List<Product>> GetProductsByName(string name);
        Task<List<Product>> GetProductsByCategoryName(string categoryName);
        Task<int> AddProducts(Product product);
        Task<bool> UpdateProducts(Product product);
        Task<bool> DeleteProduct(string id);
    }
}
