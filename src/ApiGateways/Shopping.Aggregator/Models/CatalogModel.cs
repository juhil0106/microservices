namespace Shopping.Aggregator.Models
{
    public class CatalogModel
    {
        public string? Id { get; set; }
        public string Name { get; set; } = null!;
        public string Category { get; set; } = null!;
        public int AvilableQuantity { get; set; }
        public decimal Price { get; set; }
        public string ImagePath { get; set; } = null!;
        public string Description { get; set; } = null!;
    }
}
