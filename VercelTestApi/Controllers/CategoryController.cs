using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using VercelTestApi.Models;
using VercelTestApi.Models.DTO.CategoryDTO;
using VercelTestApi.Models.DTO.ProductDTO;
using VercelTestApi.Repository.CategoryRepository;
using VercelTestApi.Repository.ProductRepository;

namespace VercelTestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryRepository categoryRepository, IProductRepository productRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _productRepository = productRepository;
            _mapper = mapper;
        }

        // GET: api/Category
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> GetCategories()
        {
            var category = await _categoryRepository.GetAllAsync();
            var categoryDtos = _mapper.Map<IEnumerable<CategoryDto>>(category);
            return Ok(categoryDtos);
        }

        // GET: api/Category/5
        [HttpGet("{id}")]
        public async Task<ActionResult> GetCategoryById(Guid id)
        {
            if(id == Guid.Empty)
            {
                return BadRequest("Invalid Category Id.");
            }

            var category = _categoryRepository.GetByIdAsync(id);

            if (category == null)
                return NotFound();

            var products = await _productRepository.GetProductsByCategoryAsync(id);
            var categoryDto = _mapper.Map<CategoryDto>(category);
            categoryDto.Products = _mapper.Map<ICollection<ProductDto>>(products);

            return Ok(categoryDto);
        }

        // POST: api/Category
        [HttpPost]
        public async Task<ActionResult> CreateCategory(CreateCategoryDto createCategoryDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var category = _mapper.Map<Category>(createCategoryDto);
            category.CategoryId = Guid.NewGuid();

            await _categoryRepository.CreateAsync(category);
            await _categoryRepository.SaveAsync();

            var categoryDto = _mapper.Map<CategoryDto>(category);
            return CreatedAtAction(nameof(GetCategoryById), new { id = category.CategoryId }, category);
        }

        // PUT: api/Category/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(Guid id, [FromBody] UpdateCategoryDto updateCategoryDto)
        {
            if (id == Guid.Empty || updateCategoryDto == null)
            {
                return BadRequest("Invalid Request.");
            }

            var existingCategory = await _categoryRepository.GetByIdAsync(id);
            if(existingCategory == null)
            {
                return NotFound();
            }

            _mapper.Map(updateCategoryDto, existingCategory);

            // Update properties
            existingCategory.CategoryName = updateCategoryDto.CategoryName;

            await _categoryRepository.UpdateAsync(existingCategory);
            await _categoryRepository.SaveAsync();

            return NoContent();
        }

        // DELETE: api/Category/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest("Invalid Category ID.");
            }

            var category = await _categoryRepository.GetByIdAsync(id);
            if(category == null)
            {
                return NotFound();
            }

            await _categoryRepository.DeleteAsync(category);
            await _categoryRepository.SaveAsync();

            return NoContent();
        }
    }
}
