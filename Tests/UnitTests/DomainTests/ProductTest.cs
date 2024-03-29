using Domain.CategoryAggregate.ValueObjects;
using Domain.CategoryAggregate;
using Domain.ProductAggregate;
using Xunit;
using Domain.ProductAggregate.ValueObjects;
using Domain.Common.Exceptions;
using Domain;

namespace Tests.UnitTests.DomainTest
{
    public class ProductTest
    {
        [Fact]
        public void CreateProduct_WhenDataIsValid_ShouldReturnProduct()
        {

            Product product = Product.Create(DomainUtils.ProductName, DomainUtils.ProductDescription, DomainUtils.ProductPrice, DomainUtils.Category, DomainUtils.ProductImages);

            Assert.IsType<ProductId>(product.Id);
            Assert.Equal(DomainUtils.ProductName, product.Name.value);
            Assert.Equal(DomainUtils.ProductDescription, product.Description);
            Assert.Equal(DomainUtils.ProductPrice, product.Price.value);
            Assert.Equal(DomainUtils.Category, product.Category);
            Assert.Equal(DomainUtils.Category.Id.value, product.CategoryId.value);
            Assert.Equal(DomainUtils.ProductImages, product.Images);
            Assert.IsType<Product>(product);
        }

        [Fact]
        public void CreateProductIdUnique_ShouldReturnProductId()
        {
            ProductId productId = ProductId.CreateUnique();

            Assert.IsType<ProductId>(productId);
        }

        [Fact]
        public void CreateProductId_ShouldReturnProductId()
        {
            ProductId productId = ProductId.Create(Guid.NewGuid());

            Assert.IsType<ProductId>(productId);
        }

        [Fact]
        public void CreateProductName_WhenNameIsNotEmpty_ShouldReturnProductName()
        {
            ProductName productName = ProductName.CreateName(DomainUtils.ProductName);

            Assert.Equal(DomainUtils.ProductName, productName.value);
            Assert.IsType<ProductName>(productName);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        public void CreateProductName_WhenNameIsEmpty_ShouldThrowException(string name)
        {
            Assert.Throws<ValidationException>(() => { ProductName.CreateName(name); });
        }

        [Fact]
        public void CreateProductPrice_WhenPriceIsPositive_ShouldReturnProductPrice()
        {
            ProductPrice productPrice = ProductPrice.CreatePrice(DomainUtils.ProductPrice);

            Assert.Equal(DomainUtils.ProductPrice, productPrice.value);
            Assert.IsType<ProductPrice>(productPrice);
        }

        [Theory]
        [InlineData(-1)]
        public void CreateProductPrice_WhenPriceIsNegative_ShouldThrowException(float price)
        {
            Assert.Throws<ValidationException>(() => { ProductPrice.CreatePrice(price); });
        }
    }
}

