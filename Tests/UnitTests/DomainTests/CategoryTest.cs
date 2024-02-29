using Domain.CategoryAggregate.ValueObjects;
using Domain.CategoryAggregate;
using Domain;
using Xunit;
using Domain.Common.Exceptions;

namespace Tests.UnitTests.DomainTest
{
    public class CategoryTest
    {
        [Fact]
        public void Create_Category_Should_Return_Category()
        {

            Category category = Category.Create(DomainUtils.CategoryName, DomainUtils.CategoryImage);

            Assert.IsType<CategoryId>(category.Id);
            Assert.Equal(DomainUtils.CategoryName, category.Name.value);
            Assert.Equal(DomainUtils.CategoryImage, category.Image);
            Assert.IsType<Category>(category);
        }

        [Fact]
        public void Create_Category_Id_Unique_Should_Return_Category_Id()
        {
            CategoryId categoryId = CategoryId.CreateUnique();

            Assert.IsType<CategoryId>(categoryId);
        }

        [Fact]
        public void Create_Category_Id_Should_Return_Category_Id()
        {
            CategoryId categoryId = CategoryId.Create(Guid.NewGuid());

            Assert.IsType<CategoryId>(categoryId);
        }

        [Fact]
        public void Create_Category_Name_Should_Return_Category_Name()
        {
            CategoryName categoryName = CategoryName.CreateName(DomainUtils.CategoryName);

            Assert.Equal(DomainUtils.CategoryName, categoryName.value);
            Assert.IsType<CategoryName>(categoryName);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        public void Create_Category_Name_Should_Throw_Exception_On_Empty_Name(string name)
        {
            Assert.Throws<ValidationException>(() => { CategoryName.CreateName(name); });
        }
    }
}
