using System.ComponentModel.DataAnnotations;

namespace VercelTestApi.Models
{
    public class Product
    {
        [Key]
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public bool InStock { get; set; }

        public Guid CategoryId { get; set; }
        public Category Category { get; set; }

    }
}
