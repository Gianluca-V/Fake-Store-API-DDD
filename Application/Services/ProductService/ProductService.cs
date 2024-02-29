using Application.Common.Interfaces.Persistence;
using Application.Services.CategoryService;
using Domain.CategoryAggregate;
using Domain.CategoryAggregate.ValueObjects;
using Domain.Common.Exceptions;
using Domain.ProductAggregate;
using Domain.ProductAggregate.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.ProductService
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository productRepository;
        private readonly ICategoryRepository categoryRepository;
        public ProductService(IProductRepository ProductRepository, ICategoryRepository CategoryRepository)
        {
            productRepository = ProductRepository;
            categoryRepository = CategoryRepository;
        }

        public async Task<ProductResult> GetProduct(string productId)
        {
            var product = await productRepository.GetProductById(productId) ?? throw new NotFoundException("Product ID not found");
            return new ProductResult(product);
        }

        public async Task<IEnumerable<ProductResult>> GetProducts()
        {
            List<Product> products = await productRepository.GetProducts();
            return products.Select(product => new ProductResult(product));
        }

        public async Task<ProductResult> CreateProduct(string Name, string Description, float Price, string Category, List<string> Images)
        {
            var category = await categoryRepository.GetCategoryByName(Category) ?? throw new BadRequestException("Category does not exist");

            Product product = Product.Create(Name, Description, Price, category, Images);
            await productRepository.CreateProduct(product);

            return new ProductResult(product);
        }

        public async Task<ProductResult> UpdateProduct(string Id,string Name, string Description, float Price, string Category, List<string> Images)
        {
            if (await productRepository.GetProductById(Id) is null) throw new NotFoundException("Product ID not found");
            var category = await categoryRepository.GetCategoryByName(Category) ?? throw new BadRequestException("Category not found");

            Product product = await productRepository.UpdateProduct(Id,Name,Description,Price, category, Images);

            return new ProductResult(product);
        }

        public async Task DeleteProduct(string productId)
        {
            if (await productRepository.GetProductById(productId) is null) throw new NotFoundException("Product ID not found");
            await productRepository.DeleteProduct(productId);
        }
    }
}
