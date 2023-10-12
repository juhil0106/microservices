using Dapper;
using Discount.Grpc.Entities;
using Npgsql;

namespace Discount.Grpc.Repositories
{
    public class DiscountRepository : IDiscountRepository
    {
        private readonly IConfiguration _configuration;
        private NpgsqlConnection _connection;

        public DiscountRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _connection = new NpgsqlConnection(_configuration["DatabaseSettings:ConnectionString"]);
        }

        public async Task<Coupon> GetDiscount(string productName)
        {
            var coupon = await _connection.QueryFirstOrDefaultAsync<Coupon>("SELECT * FROM Coupon WHERE Lower(ProductName) = @ProductName", new { ProductName = productName.ToLower() });

            if (coupon == null)
            {
                return new Coupon { ProductName = "No Discount", Amount = 0, Description = "No Discount Desc" };
            }

            return coupon;
        }

        public async Task<bool> CreateCoupon(Coupon coupon)
        {
            var affected = await _connection.ExecuteAsync("INSERT INTO Coupon (ProductName, Description, Amount) VALUES (@ProductName, @Description, @Amount)", new { ProductName = coupon.ProductName, Description = coupon.Description, Amount = coupon.Amount });

            if (affected == 0)
                return false;

            return true;
        }

        public async Task<bool> UpdateCoupon(Coupon coupon)
        {
            var affected = await _connection.ExecuteAsync("UPDATE Coupon SET ProductName = @ProductName, Amount = @Amount, Description = @Description WHERE Id = @Id", new { Id = coupon.Id, ProductName = coupon.ProductName, Amount = coupon.Amount, Description = coupon.Description });

            if (affected == 0)
                return false;

            return true;
        }

        public async Task<bool> DeleteCoupon(string productName)
        {
            var affected = await _connection.ExecuteAsync("DELETE FROM Coupon WHERE Lower(ProductName) = @ProductName", new { ProductName = productName.ToLower() });

            if (affected == 0)
                return false;

            return true;
        }
    }
}
