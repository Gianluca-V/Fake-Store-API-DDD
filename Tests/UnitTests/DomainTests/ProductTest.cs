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
        public void Create_Product_Should_Return_Product()
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
        public void Create_Product_Id_Unique_Should_Return_Product_Id()
        {
            ProductId productId = ProductId.CreateUnique();

            Assert.IsType<ProductId>(productId);
        }

        [Fact]
        public void Create_Product_Id_Should_Return_Product_Id()
        {
            ProductId productId = ProductId.Create(Guid.NewGuid());

            Assert.IsType<ProductId>(productId);
        }

        [Fact]
        public void Create_Product_Name_Should_Return_Product_Name()
        {
            ProductName productName = ProductName.CreateName(DomainUtils.ProductName);

            Assert.Equal(DomainUtils.ProductName, productName.value);
            Assert.IsType<ProductName>(productName);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        public void Create_Product_Name_Should_Throw_Exception_On_Empty_Name(string name)
        {
            Assert.Throws<ValidationException>(() => { ProductName.CreateName(name); });
        }

        [Fact]
        public void Create_Product_Price_Should_Return_Product_Price()
        {
            ProductPrice productPrice = ProductPrice.CreatePrice(DomainUtils.ProductPrice);

            Assert.Equal(DomainUtils.ProductPrice, productPrice.value);
            Assert.IsType<ProductPrice>(productPrice);
        }

        [Theory]
        [InlineData(-1)]
        public void Create_Product_Price_Should_Throw_Exception_On_Negative_Price(float price)
        {
            Assert.Throws<ValidationException>(() => { ProductPrice.CreatePrice(price); });
        }
    }
}

