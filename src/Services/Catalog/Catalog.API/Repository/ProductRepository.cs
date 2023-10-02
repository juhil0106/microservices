using Catalog.API.Context;
using Catalog.API.Entities;
using MongoDB.Driver;

namespace Catalog.API.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ICatalogContext _context;

        public ProductRepository(ICatalogContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetProducts()
        {
            var products = await _context.Products.Find(x => true).ToListAsync();
            return products;
        }

        public async Task<Product> GetProductsById(string id)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(x => x.Id, id);

            var product = await _context.Products.Find(filter).FirstOrDefaultAsync();
            return product;
        }

        public async Task<List<Product>> GetProductsByName(string name)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(x => x.Name, name);

            var products = await _context.Products.Find(filter).ToListAsync();
            return products;
        }

        public async Task<List<Product>> GetProductsByCategoryName(string categoryName)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(x => x.Category, categoryName);

            var products = await _context.Products.Find(filter).ToListAsync();
            return products;
        }

        public async Task<int> AddProducts(Product product)
        {
            if (_context.Products.Find(x => x.Name == product.Name && x.Price == product.Price).Any())
                return 0;

            await _context.Products.InsertOneAsync(product);
            return 1;
        }

        public async Task<bool> UpdateProducts(Product product)
        {
            var updateResult = await _context.Products.ReplaceOneAsync(x => x.Id == product.Id, replacement: product);

            return updateResult.IsAcknowledged && updateResult.MatchedCount > 0;
        }

        public async Task<bool> DeleteProduct(string id)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(x => x.Id, id);

            var deletedProducts = await _context.Products.DeleteOneAsync(filter);

            return deletedProducts.IsAcknowledged && deletedProducts.DeletedCount > 0;
        }
    }
}
