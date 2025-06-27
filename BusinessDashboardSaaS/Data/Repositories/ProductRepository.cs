using BusinessDashboardSaaS.Data.Interfaces;
using BusinessDashboardSaaS.Models;
using Dapper;
using Microsoft.Data.SqlClient;

namespace BusinessDashboardSaaS.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IConfiguration _config;
        private readonly string _connStr;

        public ProductRepository(IConfiguration config)
        {
            _config = config;
            _connStr = _config.GetConnectionString("MSSQLConnection");
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            using var conn = new SqlConnection(_connStr);
            return await conn.QueryAsync<Product>("SELECT * FROM Products ORDER BY CreatedAt DESC");
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            using var conn = new SqlConnection(_connStr);
            return await conn.QueryFirstOrDefaultAsync<Product>("SELECT * FROM Products WHERE ProductId = @id", new { id });
        }

        public async Task<int> AddAsync(Product product)
        {
            using var conn = new SqlConnection(_connStr);
            var sql = @"INSERT INTO Products (Name, Category, Price, StockQty)
                    VALUES (@Name, @Category, @Price, @StockQty)";
            return await conn.ExecuteAsync(sql, product);
        }

        public async Task<bool> UpdateAsync(Product product)
        {
            using var conn = new SqlConnection(_connStr);
            var sql = @"UPDATE Products SET Name=@Name, Category=@Category, Price=@Price, StockQty=@StockQty WHERE ProductId=@ProductId";
            return await conn.ExecuteAsync(sql, product) > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using var conn = new SqlConnection(_connStr);
            return await conn.ExecuteAsync("DELETE FROM Products WHERE ProductId = @id", new { id }) > 0;
        }
    }

}
