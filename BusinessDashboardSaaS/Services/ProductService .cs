using BusinessDashboardSaaS.Data.Interfaces;
using BusinessDashboardSaaS.Models;

namespace BusinessDashboardSaaS.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repo;

        public ProductService(IProductRepository repo)
        {
            _repo = repo;
        }

        public Task<IEnumerable<Product>> GetAllAsync() => _repo.GetAllAsync();
        public Task<Product?> GetByIdAsync(int id) => _repo.GetByIdAsync(id);

        public async Task<bool> SaveAsync(Product product)
        {
            if (product.ProductId == 0)
                return await _repo.AddAsync(product) > 0;
            else
                return await _repo.UpdateAsync(product);
        }

        public Task<bool> DeleteAsync(int id) => _repo.DeleteAsync(id);
    }

}
