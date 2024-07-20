using AutoMapper;
using VercelTestApi.Models.DTO.CategoryDTO;
using VercelTestApi.Models.DTO.ProductDTO;

namespace VercelTestApi.Models.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Create Mapping between DTOs and entities
            CreateMap<Category, CreateCategoryDto>().ReverseMap();

            CreateMap<Product, ProductDto>().ReverseMap();
        }
    }
}
