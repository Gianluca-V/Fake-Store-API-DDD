using Application.Services.CategoryService;
using Application.Services.ProductService;
using Contracts.Category;
using Contracts.Product;
using MapsterMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("Products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IProductService productService;
        public ProductsController(IMapper mapper, IProductService ProductService)
        {
            this.mapper = mapper;
            productService = ProductService;
        }

        /*[HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            IEnumerable<ProductResult> productsResult = await productService.GetProducts();
            List<ProductResponse> productsResponse = productsResult
                .Select(item => mapper.Map<ProductResponse>(item))
                .ToList();
            return Ok(productsResponse);
        }*/

        [HttpGet]
        public async Task<IActionResult> GetProducts(int page = 1, int pageSize = 10)
        {
            ProductPagedList productsResult = await productService.GetProducts(page, pageSize);

            List<ProductResponse> productsResponse = productsResult.PagedList.Items
                .Select(item => mapper.Map<ProductResponse>(item))
                .ToList();

            PagedList pagedList = mapper.Map<PagedList>(productsResult.PagedList);

            ProductPaginatedResponse productPaginatedResponse = new ProductPaginatedResponse(productsResponse, pagedList);
            return Ok(productPaginatedResponse);
        }

        [HttpGet]
        [Route("{productId}")]
        public async Task<IActionResult> GetProduct([FromRoute] string productId) 
        {
            var productResult = await productService.GetProduct(productId);
            var productResponse = mapper.Map<ProductResponse>(productResult);
            return Ok(productResponse);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductRequest request)
        {
            if (!ModelState.IsValid) return BadRequest();
            var productResult = await productService.CreateProduct(request.name, request.description, request.price, request.category, request.images);
            
            var productResponse = mapper.Map<ProductResponse>(productResult);

            return Ok(productResponse);
        }

        [HttpPut]
        [Authorize]
        [Route("{productId}")]
        public async Task<IActionResult> UpdateProduct([FromRoute] string productId, UpdateProductRequest request)
        {
            if (!ModelState.IsValid) return BadRequest();
            var productResult = await productService.UpdateProduct(productId, request.name, request.description, request.price, request.category, request.images);
            var productResponse = mapper.Map<ProductResponse>(productResult);

            return Ok(productResponse);
        }

        [HttpDelete]
        [Authorize]
        [Route("{productId}")]
        public async Task<IActionResult> DeleteCategory([FromRoute] string productId)
        {
            await productService.DeleteProduct(productId);
            return NoContent();
        }
    }
}
