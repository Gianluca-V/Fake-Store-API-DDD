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
        public void CreateCategory_WhenDataIsValid_ShouldReturnCategory()
        {
            //Arrange
            //Act
            Category category = Category.Create(DomainUtils.CategoryName, DomainUtils.CategoryImage);

            //Assert
            Assert.IsType<CategoryId>(category.Id);
            Assert.Equal(DomainUtils.CategoryName, category.Name.value);
            Assert.Equal(DomainUtils.CategoryImage, category.Image);
            Assert.IsType<Category>(category);
        }

        [Fact]
        public void CreateUniqueCategoryId_ShouldReturnCategoryId()
        {
            //Arrange
            //Act
            CategoryId categoryId = CategoryId.CreateUnique();

            //Assert
            Assert.IsType<CategoryId>(categoryId);
        }

        [Fact]
        public void CreateCategoryId_ShouldReturnCategoryId()
        {
            //Arrange
            //Act
            CategoryId categoryId = CategoryId.Create(Guid.NewGuid());

            //Assert
            Assert.IsType<CategoryId>(categoryId);
        }

        [Fact]
        public void CreateCategoryName_WhenNameIsNotEmpty_ShouldReturnCategoryName()
        {
            //Arrange
            //Act
            CategoryName categoryName = CategoryName.CreateName(DomainUtils.CategoryName);

            //Assert
            Assert.Equal(DomainUtils.CategoryName, categoryName.value);
            Assert.IsType<CategoryName>(categoryName);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        public void CreateCategoryName_WhenNameIsEmpty_ShouldThrowException(string name)
        {
            //Arrange
            //Act
            //Assert
            Assert.Throws<ValidationException>(() => { CategoryName.CreateName(name); });
        }
    }
}
