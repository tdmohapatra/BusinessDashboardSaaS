using BusinessDashboardSaaS.Models;

namespace BusinessDashboardSaaS.Services
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product?> GetByIdAsync(int id);
        Task<bool> SaveAsync(Product product);
        Task<bool> DeleteAsync(int id);
    }

}
