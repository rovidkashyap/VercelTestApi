using System.Linq;
using Microsoft.EntityFrameworkCore;
using VercelTestApi.Models;
using VercelTestApi.Models.DataContext;
using VercelTestApi.Repository.GenericRepository;

namespace VercelTestApi.Repository.ProductRepository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly VercelDbContext _context;

        public ProductRepository(VercelDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetProductsByCategoryAsync(Guid categoryId)
        {
            var products = await _context.Products.Where(p => p.CategoryId == categoryId).ToListAsync();

            return products;
        }
    }
}
