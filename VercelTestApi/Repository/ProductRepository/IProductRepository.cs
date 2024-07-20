using VercelTestApi.Models;
using VercelTestApi.Repository.GenericRepository;

namespace VercelTestApi.Repository.ProductRepository
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<IEnumerable<Product>> GetProductsByCategoryAsync(Guid categoryId);
    }
}
