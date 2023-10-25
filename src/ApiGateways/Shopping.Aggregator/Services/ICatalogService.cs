using Shopping.Aggregator.Models;

namespace Shopping.Aggregator.Services
{
    public interface ICatalogService
    {
        Task<CatalogModel> GetCatalogById(string id);
    }
}
