using Application.Services.CategoryService;
using Application.Services.ProductService;
using Contracts.Category;
using Contracts.Product;
using MapsterMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("Categories")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly ICategoryService categoryService;
        public CategoriesController(IMapper mapper, ICategoryService CategoryService)
        {
            this.mapper = mapper;
            categoryService = CategoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            IEnumerable<CategoryResult> categoriesResult = await categoryService.GetCategories();
            List<CategoryResponse> categoryResponse = categoriesResult
                .Select(item => mapper.Map<CategoryResponse>(item))
                .ToList();
            return Ok(categoryResponse);

        }

        /*[HttpGet]
        [Route("/{categoryId}")]
        public IActionResult GetCategoryById(string id)
        {
            var categoryResult = categoryService.GetCategoryById(id);
            var categoryResponse = mapper.Map<CategoryResponse>(categoryResult);
            return Ok(categoryResponse);
        }*/

        [HttpGet]
        [Route("{categoryName}")]
        public async Task<IActionResult> GetCategoryByName([FromRoute] string categoryName)
        {
            var categoryResult = await categoryService.GetCategoryByName(categoryName);
            var categoryResponse = mapper.Map<CategoryResponse>(categoryResult);
            return Ok(categoryResponse);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryRequest request)
        {
            if (!ModelState.IsValid) return BadRequest();
            var categoryResult = await categoryService.CreateCategory(request.name, request.image);

            var categoryResponse = mapper.Map<CategoryResponse>(categoryResult);
            return Ok(categoryResponse);
        }

        [HttpPut]
        [Authorize]
        [Route("{categoryId}")]
        public async Task<IActionResult> UpdateCategory([FromRoute] string categoryId, [FromBody] UpdateCategoryRequest request)
        {
            if (!ModelState.IsValid) return BadRequest();
            var categoryResult = await categoryService.UpdateCategory(categoryId, request.name, request.image);
            var categoryResponse = mapper.Map<CategoryResponse>(categoryResult);

            return Ok(categoryResponse);
        }

        [HttpDelete]
        [Authorize]
        [Route("{categoryName}")]
        public async Task<IActionResult> DeleteCategory([FromRoute] string categoryName)
        {
            await categoryService.DeleteCategory(categoryName);
            return NoContent();
        }
    }
}
