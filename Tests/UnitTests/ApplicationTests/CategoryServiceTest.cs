using Application.Services.CategoryService;
using Domain.CategoryAggregate;
using Domain.Common.Exceptions;
using Domain.CategoryAggregate.ValueObjects;
using Domain.CategoryAggregate;
using Infrastructure.Persistence.Repositories;
using Infrastructure.Persistence;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Application.Services.CategoryService;

namespace Tests.UnitTests.ApplicationTests
{
    public class CategoryServiceTest
    {
        [Fact]
        public async void GetCategories_WhenThereAreCategories_ShouldReturnCategoryResult()
        {
            //Arrange
            var dbContext = new Mock<FakeStoreAPIDbContext>().Object;
            var categoryRepository = new Mock<CategoryRepository>(dbContext);
            var categoryService = new CategoryService(categoryRepository.Object);

            List<Category> expectedCategories = new List<Category>()
            {
                ApplicationUtils.GetRandomCategory(),
                ApplicationUtils.GetRandomCategory(),
                ApplicationUtils.GetRandomCategory()
            };

            categoryRepository.Setup(x => x.GetCategories()).ReturnsAsync(expectedCategories);

            //Act
            IEnumerable<CategoryResult> actualCategories = await categoryService.GetCategories();

            //Assert
            Assert.NotEmpty(actualCategories);
            for (int i = 0; i < expectedCategories.Count; i++)
            {
                Assert.Equal(actualCategories.ElementAt(i).category, expectedCategories.ElementAt(i));
            }
        }

        [Fact]
        public async void GetCategories_WhenThereAreNoCategories_ShouldReturnEmptyList()
        {
            //Arrange
            var dbContext = new Mock<FakeStoreAPIDbContext>().Object;
            var categoryRepository = new Mock<CategoryRepository>(dbContext);
            var categoryService = new CategoryService(categoryRepository.Object);

            List<Category> expectedCategories = new List<Category>();

            categoryRepository.Setup(x => x.GetCategories()).ReturnsAsync(expectedCategories);

            //Act
            IEnumerable<CategoryResult> actualCategories = await categoryService.GetCategories();

            //Assert
            Assert.Empty(actualCategories);
        }

        [Fact]
        public async void GetCategoryById_WhenIdDoesExist_ShouldReturnCategoryResult()
        {
            //Arrange
            var dbContext = new Mock<FakeStoreAPIDbContext>().Object;
            var categoryRepository = new Mock<CategoryRepository>(dbContext);
            var categoryService = new CategoryService(categoryRepository.Object);

            Category expectedCategory = ApplicationUtils.Category;

            categoryRepository.Setup(x => x.GetCategoryById(It.IsAny<string>())).ReturnsAsync(expectedCategory);

            //Act
            CategoryResult actualCategory = await categoryService.GetCategoryById(ApplicationUtils.Category.Id.value.ToString());

            //Assert
            Assert.IsType<CategoryResult>(actualCategory);
            Assert.NotNull(actualCategory);
            Assert.NotNull(actualCategory.category);
            Assert.True(expectedCategory == actualCategory.category);
        }

        [Fact]
        public async void GetCategoryById_WhenIdDoesNotExist_ShouldThrowNotFoundException()
        {
            //Arrange
            var dbContext = new Mock<FakeStoreAPIDbContext>().Object;
            var categoryRepository = new Mock<CategoryRepository>(dbContext);
            var categoryService = new CategoryService(categoryRepository.Object);

            Category expectedCategory = null;

            categoryRepository.Setup(x => x.GetCategoryById(It.IsAny<string>())).ReturnsAsync(expectedCategory);

            //Act
            var exception = await Assert.ThrowsAsync<NotFoundException>(async () => await categoryService.GetCategoryById(ApplicationUtils.Category.Id.value.ToString()));

            //Assert
            Assert.NotNull(exception);
            Assert.Equal("Category ID not found", exception.Message);
        }

        [Fact]
        public async void GetCategoryByName_WhenNameDoesExist_ShouldReturnCategoryResult()
        {
            //Arrange
            var dbContext = new Mock<FakeStoreAPIDbContext>().Object;
            var categoryRepository = new Mock<CategoryRepository>(dbContext);
            var categoryService = new CategoryService(categoryRepository.Object);

            Category expectedCategory = ApplicationUtils.Category;

            categoryRepository.Setup(x => x.GetCategoryByName(It.IsAny<string>())).ReturnsAsync(expectedCategory);

            //Act
            CategoryResult actualCategory = await categoryService.GetCategoryByName(ApplicationUtils.Category.Name.value.ToString());

            //Assert
            Assert.IsType<CategoryResult>(actualCategory);
            Assert.NotNull(actualCategory);
            Assert.NotNull(actualCategory.category);
            Assert.True(expectedCategory == actualCategory.category);
        }

        [Fact]
        public async void GetCategoryByName_WhenNameDoesNotExist_ShouldThrowNotFoundException()
        {
            //Arrange
            var dbContext = new Mock<FakeStoreAPIDbContext>().Object;
            var categoryRepository = new Mock<CategoryRepository>(dbContext);
            var categoryService = new CategoryService(categoryRepository.Object);

            Category expectedCategory = null;

            categoryRepository.Setup(x => x.GetCategoryByName(It.IsAny<string>())).ReturnsAsync(expectedCategory);

            //Act
            var exception = await Assert.ThrowsAsync<NotFoundException>(async () => await categoryService.GetCategoryByName(ApplicationUtils.Category.Name.value.ToString()));

            //Assert
            Assert.NotNull(exception);
            Assert.Equal("Category not found", exception.Message);
        }

        [Fact]
        public async void CreateCategory_WhenCategoryDoesNotExist_ShouldReturnCategoryResult()
        {
            //Arrange
            var dbContext = new Mock<FakeStoreAPIDbContext>().Object;
            var categoryRepository = new Mock<CategoryRepository>(dbContext);
            var categoryService = new CategoryService(categoryRepository.Object);

            Category expectedCategory = null;

            categoryRepository.Setup(x => x.GetCategoryByName(It.IsAny<string>())).ReturnsAsync(expectedCategory);
            categoryRepository.Setup(x => x.CreateCategory(It.IsAny<Category>()));

            //Act
            CategoryResult actualCategory = await categoryService.CreateCategory(ApplicationUtils.CategoryName, ApplicationUtils.CategoryImage);

            //Assert
            Assert.IsType<CategoryResult>(actualCategory);
            Assert.NotNull(actualCategory);
            Assert.NotNull(actualCategory.category);
        }

        [Fact]
        public async void CreateCategory_WhenCategoryDoesExist_ShouldThrowBadRequestException()
        {
            //Arrange
            var dbContext = new Mock<FakeStoreAPIDbContext>().Object;
            var categoryRepository = new Mock<CategoryRepository>(dbContext);
            var categoryService = new CategoryService(categoryRepository.Object);

            Category expectedCategory = ApplicationUtils.Category;

            categoryRepository.Setup(x => x.GetCategoryByName(It.IsAny<string>())).ReturnsAsync(expectedCategory);
            categoryRepository.Setup(x => x.CreateCategory(It.IsAny<Category>()));

            //Act
            var exception = await Assert.ThrowsAsync<AlreadyExistException>(async () => await categoryService.CreateCategory(ApplicationUtils.CategoryName,ApplicationUtils.CategoryImage));

            //Assert
            Assert.NotNull(exception);
            Assert.Equal("Category already exist", exception.Message);
        }

        [Fact]
        public async void UpdateCategory_WhenCategoryDoesExist_ShouldReturnCategoryResult()
        {
            //Arrange
            var dbContext = new Mock<FakeStoreAPIDbContext>().Object;
            var categoryRepository = new Mock<CategoryRepository>(dbContext);
            var categoryService = new CategoryService(categoryRepository.Object);

            Category expectedCategory = ApplicationUtils.GetRandomCategory();

            categoryRepository.Setup(x => x.UpdateCategory(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(expectedCategory);
            categoryRepository.Setup(x => x.GetCategoryById(It.IsAny<string>())).ReturnsAsync(ApplicationUtils.GetRandomCategory());

            //Act
            var actualCategory = await categoryService.UpdateCategory(expectedCategory.Id.value.ToString(), ApplicationUtils.CategoryName, ApplicationUtils.CategoryImage);

            //Assert
            Assert.IsType<CategoryResult>(actualCategory);
            Assert.NotNull(actualCategory);
            Assert.NotNull(actualCategory.category);
            Assert.Equal(expectedCategory, actualCategory.category);
        }

        [Fact]
        public async void UpdateCategory_WhenCategoryDoesNotExist_ShouldThrowNotFoundException()
        {
            //Arrange
            var dbContext = new Mock<FakeStoreAPIDbContext>().Object;
            var categoryRepository = new Mock<CategoryRepository>(dbContext);
            var categoryService = new CategoryService( categoryRepository.Object);

            Category expectedCategory = null;


            categoryRepository.Setup(x => x.UpdateCategory(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(expectedCategory);
            categoryRepository.Setup(x => x.GetCategoryById(It.IsAny<string>())).ReturnsAsync(expectedCategory);

            //Act
            var exception = await Assert.ThrowsAsync<NotFoundException>(async () => await categoryService.UpdateCategory(Guid.NewGuid().ToString(), ApplicationUtils.CategoryName, ApplicationUtils.CategoryImage));

            //Assert
            Assert.NotNull(exception);
            Assert.Equal("Category not found", exception.Message);
        }

        [Fact]
        public async void DeleteCategory_WhenIdDoesExist_ShouldReturnTask()
        {
            //Arrange
            var dbContext = new Mock<FakeStoreAPIDbContext>().Object;
            var categoryRepository = new Mock<CategoryRepository>(dbContext);
            var categoryService = new CategoryService(categoryRepository.Object);

            Category expectedCategory = ApplicationUtils.Category;

            categoryRepository.Setup(x => x.GetCategoryById(It.IsAny<string>())).ReturnsAsync(expectedCategory);
            categoryRepository.Setup(x => x.DeleteCategory(It.IsAny<string>())).ReturnsAsync(true);


            // Act
            await categoryService.DeleteCategory(ApplicationUtils.Category.Id.value.ToString());

            // Assert
            categoryRepository.Verify(x => x.GetCategoryById(ApplicationUtils.Category.Id.value.ToString()), Times.Once);
            categoryRepository.Verify(x => x.DeleteCategory(ApplicationUtils.Category.Id.value.ToString()), Times.Once);

            categoryRepository.VerifyNoOtherCalls();
        }

        [Fact]
        public async void DeleteCategory_WhenIdDoesNotExist_ShouldThrowNotFoundException()
        {
            //Arrange
            var dbContext = new Mock<FakeStoreAPIDbContext>().Object;
            var categoryRepository = new Mock<CategoryRepository>(dbContext);
            var categoryService = new CategoryService(categoryRepository.Object);

            Category expectedCategory = null;

            categoryRepository.Setup(x => x.GetCategoryById(It.IsAny<string>())).ReturnsAsync(expectedCategory);

            //Act
            var exception = await Assert.ThrowsAsync<NotFoundException>(async () => await categoryService.DeleteCategory(ApplicationUtils.Category.Id.value.ToString()));

            //Assert
            Assert.NotNull(exception);
            Assert.Equal("Category not found", exception.Message);
        }
    }
}
