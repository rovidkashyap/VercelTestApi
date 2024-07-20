using VercelTestApi.Models;
using VercelTestApi.Models.DataContext;
using VercelTestApi.Repository.GenericRepository;

namespace VercelTestApi.Repository.CategoryRepository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(VercelDbContext context) : base(context)
        {
            
        }
    }
}
