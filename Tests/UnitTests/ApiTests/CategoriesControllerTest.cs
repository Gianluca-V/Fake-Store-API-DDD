using Api.Controllers;
using Application.Services.CategoryService;
using Contracts.Category;
using Domain.CategoryAggregate;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Tests.UnitTests.ApiTests
{
    public class CategoriesControllerTest
    {
        [Fact]
        public async void GetCategories_WhenThereAreCategories_ShouldReturnIActionResultOK()
        {
            // Arrange
            var categoryService = new Mock<ICategoryService>();
            var mapper = new Mapper();
            List<Category> expectedCategories = new List<Category>()
            {
                ApiUtils.GetRandomCategory(),
                ApiUtils.GetRandomCategory(),
                ApiUtils.GetRandomCategory()
            };

            var expectedCategoryResults = expectedCategories.Select(c => mapper.Map<CategoryResult>(c)); 
            var expectedCategoryResponses = expectedCategoryResults.Select(r => mapper.Map<CategoryResponse>(r)).ToList();
            categoryService.Setup(s => s.GetCategories()).ReturnsAsync(expectedCategoryResults);

            var controller = new CategoriesController(mapper, categoryService.Object);

            // Act
            var actionResult = await controller.GetCategories();

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(actionResult);
            var categories = Assert.IsType<List<CategoryResponse>>(okObjectResult.Value);

            Assert.Equal(expectedCategoryResponses, categories);
        }

        [Fact]
        public async void GetCategoryByName_WhenCategoryExist_ShouldReturnIActionResultOK()
        {
            // Arrange
            var categoryService = new Mock<ICategoryService>();
            var mapper = new Mapper();
            var expectedCategory = ApiUtils.GetRandomCategory();

            var expectedCategoryResult = mapper.Map<CategoryResult>(expectedCategory);
            var expectedCategoryResponse = mapper.Map<CategoryResponse>(expectedCategoryResult);
            categoryService.Setup(s => s.GetCategoryByName(It.IsAny<string>())).ReturnsAsync(expectedCategoryResult);

            var controller = new CategoriesController(mapper, categoryService.Object);

            // Act
            var actionResult = await controller.GetCategoryByName(expectedCategory.Name.value);

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(actionResult);
            var category = Assert.IsType<CategoryResponse>(okObjectResult.Value);

            Assert.Equal(expectedCategoryResponse, category);
        }

        [Fact]
        public async Task CreateCategory_WithValidRequest_ShouldReturnIActionResultOK()
        {
            // Arrange
            var categoryService = new Mock<ICategoryService>();
            var mapper = new Mapper();
            var expectedCategory = ApiUtils.GetRandomCategory();

            var expectedCategoryResult = mapper.Map<CategoryResult>(expectedCategory);
            var expectedCategoryResponse = mapper.Map<CategoryResponse>(expectedCategoryResult);
            categoryService.Setup(s => s.CreateCategory(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(expectedCategoryResult);

            var controller = new CategoriesController(mapper, categoryService.Object);

            var createCategoryRequest = new CreateCategoryRequest(expectedCategory.Name.value, expectedCategory.Image);
            // Act
            var actionResult = await controller.CreateCategory(createCategoryRequest);

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(actionResult);
            var category = Assert.IsType<CategoryResponse>(okObjectResult.Value);

            Assert.Equal(expectedCategoryResponse, category);
        }

        [Fact]
        public async Task CreateCategory_WithInvalidRequest_ShouldReturnIActionResultBadRequest()
        {
            // Arrange
            var categoryService = new Mock<ICategoryService>();
            var mapper = new Mapper();
            var expectedCategory = ApiUtils.GetRandomCategory();

            var expectedCategoryResult = mapper.Map<CategoryResult>(expectedCategory);
            var expectedCategoryResponse = mapper.Map<CategoryResponse>(expectedCategoryResult);
            categoryService.Setup(s => s.CreateCategory(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(expectedCategoryResult);

            var controller = new CategoriesController(mapper, categoryService.Object);

            CreateCategoryRequest createCategoryRequest = new CreateCategoryRequest();
            // Act
            var actionResult = await controller.CreateCategory(createCategoryRequest);

            // Assert
            var okObjectResult = Assert.IsType<BadRequestObjectResult>(actionResult);
            var category = Assert.IsType<CategoryResponse>(okObjectResult.Value);

            Assert.Equal(expectedCategoryResponse, category);
        }
    }
}
