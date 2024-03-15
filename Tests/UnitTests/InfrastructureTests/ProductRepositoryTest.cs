using Domain.ProductAggregate;
using Domain.ProductAggregate.ValueObjects;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.UnitTests.InfrastructureTest;
using Xunit;

namespace Tests.UnitTests.InfrastructureTests
{
    public class ProductRepositoryTest
    {
        /*
        [Fact]
        public async void GetProducts_WhenThereAreProducts_ShouldReturnListOfProducts()
        {
            //Arrange
            var dbContext = new Mock<FakeStoreAPIDbContext>().Object;
            var productRepository = new Mock<ProductRepository>(dbContext);

            var expectedProducts = new List<Product>
            {
                InfrastructureUtils.GetRandomProduct(),
                InfrastructureUtils.GetRandomProduct(),
                InfrastructureUtils.GetRandomProduct()
            };
            productRepository.Setup(x => x.GetProducts()).ReturnsAsync(expectedProducts);

            //Act
            var actualProducts = await productRepository.Object.GetProducts();

            //Assert
            Assert.Equal(expectedProducts, actualProducts);
        }

        [Fact]
        public async void GetProducts_WhenThereAreNoProducts_ShouldReturnEmptyList()
        {
            //Arrange
            var dbContext = new Mock<FakeStoreAPIDbContext>().Object;
            var productRepository = new Mock<ProductRepository>(dbContext);

            var productList = new List<Product>();
            productRepository.Setup(x => x.GetProducts()).ReturnsAsync(productList);

            //Act
            var actualProducts = await productRepository.Object.GetProducts();

            //Assert
            Assert.Empty(actualProducts);
        }

        [Fact]
        public async void GetProductById_WhenIdExist_ShouldReturnProduct()
        {
            //Arrage
            var dbContext = new Mock<FakeStoreAPIDbContext>().Object;
            var productRepository = new Mock<ProductRepository>(dbContext);

            var expectedProduct = InfrastructureUtils.Product;
            productRepository.Setup(x => x.GetProductById(It.IsAny<string>())).ReturnsAsync(expectedProduct);

            //Act
            var actualProduct = await productRepository.Object.GetProductById(expectedProduct.Id.value.ToString());

            //Assert
            Assert.Equal(expectedProduct, actualProduct);
        }

        [Fact]
        public async void GetProductById_WhenIdDoesNotExist_ShouldReturnNull()
        {
            //Arrage
            var dbContext = new Mock<FakeStoreAPIDbContext>().Object;
            var productRepository = new Mock<ProductRepository>(dbContext);

            productRepository.Setup(x => x.GetProductById(It.IsAny<string>()));

            //Act
            var actualProduct = await productRepository.Object.GetProductById(Guid.NewGuid().ToString());

            //Assert
            Assert.Null(actualProduct);
        }

        [Fact]
        public async void GetProductById_WhenIdDoesNotMatchGuidFormat_ShouldReturnNull()
        {
            //Arrage
            var dbContext = new Mock<FakeStoreAPIDbContext>().Object;
            var productRepository = new Mock<ProductRepository>(dbContext);

            productRepository.Setup(x => x.GetProductById(It.IsAny<string>()));

            //Act
            var actualProduct = await productRepository.Object.GetProductById("string that is not a Guid");

            //Assert
            Assert.Null(actualProduct);
        }

        [Fact]
        public async void DeleteProduct_WhenIdExist_ShouldReturnTrue()
        {
            //Arrage
            var productSample = InfrastructureUtils.Product;
            var dbContext = new Mock<FakeStoreAPIDbContext>().Object;
            var productRepository = new Mock<ProductRepository>(dbContext);

            productRepository.Setup(x => x.DeleteProduct(It.IsAny<string>())).ReturnsAsync(true);

            //Act
            var result = await productRepository.Object.DeleteProduct(productSample.Id.ToString());

            //Assert
            Assert.True(result);
        }*/
    }
}
