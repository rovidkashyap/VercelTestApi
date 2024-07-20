using System.Security.Principal;

namespace VercelTestApi.Models.DTO.ProductDTO
{
    public class ProductDto
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public bool InStock { get; set; }
    }
}
