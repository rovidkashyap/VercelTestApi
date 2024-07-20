using VercelTestApi.Models.DTO.ProductDTO;

namespace VercelTestApi.Models.DTO.CategoryDTO
{
    public class CategoryDto
    {
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int NumberOfProducts { get; set; }

        public ICollection<ProductDto> Products { get; set; } = new List<ProductDto>();
    }
}
