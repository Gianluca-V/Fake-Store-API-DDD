using Infrastructure.Persistence.Repositories;
using Infrastructure.Persistence;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Application.Services.ProductService;
using Domain.ProductAggregate;
using Domain.Common.Exceptions;
using Domain.CategoryAggregate;
using Domain.ProductAggregate.ValueObjects;
using Application.Common.Interfaces.Persistence;

namespace Tests.UnitTests.ApplicationTests
{
    public class ProductServiceTest
    {
        [Fact]
        public async void GetProducts_WhenThereAreProducts_ShouldReturnProductResult()
        {
            //Arrange
            var productRepository = new Mock<IProductRepository>();
            var categoryRepository = new Mock<ICategoryRepository>();
            var productService = new ProductService(productRepository.Object, categoryRepository.Object);

            List<Product> expectedProducts = new List<Product>()
            {
                ApplicationUtils.GetRandomProduct(),
                ApplicationUtils.GetRandomProduct(),
                ApplicationUtils.GetRandomProduct()
            };

            productRepository.Setup(x => x.GetProducts()).ReturnsAsync(expectedProducts);
            //Act
            IEnumerable<ProductResult> actualProducts = await productService.GetProducts();

            //Assert
            Assert.NotEmpty(actualProducts);
            for (int i = 0; i < expectedProducts.Count; i++)
            {
                Assert.Equal(actualProducts.ElementAt(i).product, expectedProducts.ElementAt(i));
            }
        }

        [Fact]
        public async void GetProducts_WhenThereAreNoProducts_ShouldReturnEmptyList()
        {
            //Arrange
            var productRepository = new Mock<IProductRepository>();
            var categoryRepository = new Mock<ICategoryRepository>();
            var productService = new ProductService(productRepository.Object, categoryRepository.Object);

            List<Product> expectedProducts = new List<Product>();

            productRepository.Setup(x => x.GetProducts()).ReturnsAsync(expectedProducts);
            //Act
            IEnumerable<ProductResult> actualProducts = await productService.GetProducts();

            //Assert
            Assert.Empty(actualProducts);
        }

        [Fact]
        public async void GetProduct_WhenIdDoesExist_ShouldReturnProductResult()
        {
            //Arrange
            var productRepository = new Mock<IProductRepository>();
            var categoryRepository = new Mock<ICategoryRepository>();
            var productService = new ProductService(productRepository.Object, categoryRepository.Object);

            Product expectedProduct = ApplicationUtils.Product;

            productRepository.Setup(x => x.GetProductById(It.IsAny<string>())).ReturnsAsync(expectedProduct);

            //Act
            ProductResult actualProduct = await productService.GetProduct(ApplicationUtils.Product.Id.value.ToString());

            //Assert
            Assert.IsType<ProductResult>(actualProduct);
            Assert.NotNull(actualProduct);
            Assert.NotNull(actualProduct.product);
            Assert.True(expectedProduct == actualProduct.product);
        }

        [Fact]
        public async void GetProduct_WhenIdDoesNotExist_ShouldThrowNotFoundException()
        {
            //Arrange
            var productRepository = new Mock<IProductRepository>();
            var categoryRepository = new Mock<ICategoryRepository>();
            var productService = new ProductService(productRepository.Object, categoryRepository.Object);

            Product expectedProduct = null;

            productRepository.Setup(x => x.GetProductById(It.IsAny<string>())).ReturnsAsync(expectedProduct);

            //Act
            var exception = await Assert.ThrowsAsync<NotFoundException>(async () => await productService.GetProduct(ApplicationUtils.Product.Id.value.ToString()));
            
            //Assert
            Assert.NotNull(exception);
            Assert.Equal("Product ID not found", exception.Message);
        }

        [Fact]
        public async void CreateProduct_WhenCategoryDoesExist_ShouldReturnProductResult()
        {
            //Arrange
            var productRepository = new Mock<IProductRepository>();
            var categoryRepository = new Mock<ICategoryRepository>();
            var productService = new ProductService(productRepository.Object, categoryRepository.Object);

            Category expectedCategory = ApplicationUtils.Category;

            categoryRepository.Setup(x => x.GetCategoryByName(It.IsAny<string>())).ReturnsAsync(expectedCategory);
            productRepository.Setup(x => x.CreateProduct(It.IsAny<Product>()));

            //Act
            ProductResult actualProduct = await productService.CreateProduct(ApplicationUtils.ProductName,ApplicationUtils.ProductDescription,ApplicationUtils.ProductPrice,ApplicationUtils.CategoryName,ApplicationUtils.ProductImages);

            //Assert
            Assert.IsType<ProductResult>(actualProduct);
            Assert.NotNull(actualProduct);
            Assert.NotNull(actualProduct.product);
        }

        [Fact]
        public async void CreateProduct_WhenCategoryDoesNotExist_ShouldThrowBadRequestException()
        {
            //Arrange
            var productRepository = new Mock<IProductRepository>();
            var categoryRepository = new Mock<ICategoryRepository>();
            var productService = new ProductService(productRepository.Object, categoryRepository.Object);

            Category expectedCategory = null;

            categoryRepository.Setup(x => x.GetCategoryByName(It.IsAny<string>())).ReturnsAsync(expectedCategory);
            productRepository.Setup(x => x.CreateProduct(It.IsAny<Product>()));

            //Act
            var exception = await Assert.ThrowsAsync<BadRequestException>(async () => await productService.CreateProduct(ApplicationUtils.ProductName, ApplicationUtils.ProductDescription, ApplicationUtils.ProductPrice, ApplicationUtils.CategoryName, ApplicationUtils.ProductImages));

            //Assert
            Assert.NotNull(exception);
            Assert.Equal("Category does not exist", exception.Message);
        }

        [Fact]
        public async void UpdateProduct_WhenCategoryDoesExistAndProductDoesExist_ShouldReturnProductResult()
        {
            //Arrange
            var productRepository = new Mock<IProductRepository>();
            var categoryRepository = new Mock<ICategoryRepository>();
            var productService = new ProductService(productRepository.Object, categoryRepository.Object);

            Category expectedCategory = ApplicationUtils.Category;
            Product expectedProduct = ApplicationUtils.GetRandomProduct();
            expectedProduct.Price = ProductPrice.CreatePrice(123456789);


            categoryRepository.Setup(x => x.GetCategoryByName(It.IsAny<string>())).ReturnsAsync(expectedCategory);
            productRepository.Setup(x => x.UpdateProduct(It.IsAny<string>(),It.IsAny<string>(), It.IsAny<string>(), It.IsAny<float>(), It.IsAny<Category>(), It.IsAny<List<string>>())).ReturnsAsync(expectedProduct);
            productRepository.Setup(x => x.GetProductById(It.IsAny<string>())).ReturnsAsync(ApplicationUtils.GetRandomProduct());

            //Act
            var actualProduct = await productService.UpdateProduct(expectedProduct.Id.value.ToString(),ApplicationUtils.ProductName, ApplicationUtils.ProductDescription, 123456789, ApplicationUtils.CategoryName, ApplicationUtils.ProductImages);

            //Assert
            Assert.IsType<ProductResult>(actualProduct);
            Assert.NotNull(actualProduct);
            Assert.NotNull(actualProduct.product);
            Assert.Equal(expectedProduct, actualProduct.product);
        }

        [Fact]
        public async void UpdateProduct_WhenCategoryDoesNotExistAndProductDoesExist_ShouldThrowBadRequestException()
        {
            //Arrange
            var productRepository = new Mock<IProductRepository>();
            var categoryRepository = new Mock<ICategoryRepository>();
            var productService = new ProductService(productRepository.Object, categoryRepository.Object);

            Category expectedCategory = null;
            Product expectedProduct = ApplicationUtils.GetRandomProduct();
            expectedProduct.Price = ProductPrice.CreatePrice(123456789);


            categoryRepository.Setup(x => x.GetCategoryByName(It.IsAny<string>())).ReturnsAsync(expectedCategory);
            productRepository.Setup(x => x.UpdateProduct(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<float>(), It.IsAny<Category>(), It.IsAny<List<string>>())).ReturnsAsync(expectedProduct);
            productRepository.Setup(x => x.GetProductById(It.IsAny<string>())).ReturnsAsync(ApplicationUtils.GetRandomProduct());

            //Act
            var exception = await Assert.ThrowsAsync<BadRequestException>(async () => await productService.UpdateProduct(expectedProduct.Id.value.ToString(), ApplicationUtils.ProductName, ApplicationUtils.ProductDescription, ApplicationUtils.ProductPrice, ApplicationUtils.CategoryName, ApplicationUtils.ProductImages));

            //Assert
            Assert.NotNull(exception);
            Assert.Equal("Category not found", exception.Message);
        }

        [Fact]
        public async void UpdateProduct_WhenCategoryDoesExistAndProductDoesNotExist_ShouldThrowNotFoundException()
        {
            //Arrange
            var productRepository = new Mock<IProductRepository>();
            var categoryRepository = new Mock<ICategoryRepository>();
            var productService = new ProductService(productRepository.Object, categoryRepository.Object);

            Category expectedCategory = ApplicationUtils.Category;
            Product expectedProduct = null;


            categoryRepository.Setup(x => x.GetCategoryByName(It.IsAny<string>())).ReturnsAsync(expectedCategory);
            productRepository.Setup(x => x.UpdateProduct(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<float>(), It.IsAny<Category>(), It.IsAny<List<string>>())).ReturnsAsync(expectedProduct);
            productRepository.Setup(x => x.GetProductById(It.IsAny<string>())).ReturnsAsync(expectedProduct);

            //Act
            var exception = await Assert.ThrowsAsync<NotFoundException>(async () => await productService.UpdateProduct(Guid.NewGuid().ToString(), ApplicationUtils.ProductName, ApplicationUtils.ProductDescription, ApplicationUtils.ProductPrice, ApplicationUtils.CategoryName, ApplicationUtils.ProductImages));

            //Assert
            Assert.NotNull(exception);
            Assert.Equal("Product ID not found", exception.Message);
        }

        [Fact]
        public async void DeleteProduct_WhenIdDoesExist_ShouldReturnTask()
        {
            //Arrange
            var productRepository = new Mock<IProductRepository>();
            var categoryRepository = new Mock<ICategoryRepository>();
            var productService = new ProductService(productRepository.Object, categoryRepository.Object);

            Product expectedProduct = ApplicationUtils.Product;

            productRepository.Setup(x => x.GetProductById(It.IsAny<string>())).ReturnsAsync(expectedProduct);
            productRepository.Setup(x => x.DeleteProduct(It.IsAny<string>())).ReturnsAsync(true);


            // Act
            await productService.DeleteProduct(ApplicationUtils.Product.Id.value.ToString());

            // Assert
            productRepository.Verify(x => x.GetProductById(ApplicationUtils.Product.Id.value.ToString()), Times.Once);
            productRepository.Verify(x => x.DeleteProduct(ApplicationUtils.Product.Id.value.ToString()), Times.Once);

            productRepository.VerifyNoOtherCalls();
        }

        [Fact]
        public async void DeleteProduct_WhenIdDoesNotExist_ShouldThrowNotFoundException()
        {
            //Arrange
            var productRepository = new Mock<IProductRepository>();
            var categoryRepository = new Mock<ICategoryRepository>();
            var productService = new ProductService(productRepository.Object, categoryRepository.Object);

            Product expectedProduct = null;

            productRepository.Setup(x => x.GetProductById(It.IsAny<string>())).ReturnsAsync(expectedProduct);

            //Act
            var exception = await Assert.ThrowsAsync<NotFoundException>(async () => await productService.DeleteProduct(ApplicationUtils.Product.Id.value.ToString()));

            //Assert
            Assert.NotNull(exception);
            Assert.Equal("Product ID not found", exception.Message);
        }
    }
}
