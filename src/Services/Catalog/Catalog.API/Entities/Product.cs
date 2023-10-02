using MongoDB.Bson.Serialization.Attributes;

namespace Catalog.API.Entities
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string? Id { get; set; }
        public string Name { get; set; } = null!;
        public string Category { get; set; } = null!;
        public int AvilableQuantity { get; set; }
        public decimal Price { get; set; }
        public string ImagePath { get; set; } = null!;
        public string Disription { get; set; } = null!;
        
    }
}
