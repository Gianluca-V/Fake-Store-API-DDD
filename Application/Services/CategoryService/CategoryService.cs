using Application.Common.Interfaces.Persistence;
using Application.Services.ProductService;
using Domain.CategoryAggregate;
using Domain.Common.Exceptions;
using Domain.ProductAggregate;
using Domain.ProductAggregate.ValueObjects;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Application.Services.CategoryService
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository categoryRepository;

        public CategoryService(ICategoryRepository CategoryRepository)
        {
            categoryRepository = CategoryRepository;
        }

        public async Task<CategoryResult> CreateCategory(string Name, string Image)
        {
            if (await categoryRepository.GetCategoryByName(Name) is not null) throw new AlreadyExistException("Category already exist");
            Category category = Category.Create(Name,Image);
            await categoryRepository.CreateCategory(category);
            return new CategoryResult(category);
        }

        public async Task DeleteCategory(string CategoryName)
        {
            if (await categoryRepository.GetCategoryByName(CategoryName) is null) throw new NotFoundException("Category not found");
            await categoryRepository.DeleteCategory(CategoryName);
        }

        public async Task<IEnumerable<CategoryResult>> GetCategories()
        {
            List<Category> Categories = await categoryRepository.GetCategories();
            return Categories.Select(category => new CategoryResult(category));
        }

        public async Task<CategoryResult> GetCategoryById(string Id)
        {
            var Category = await categoryRepository.GetCategoryById(Id) ?? throw new NotFoundException("Category ID not found");
            return new CategoryResult(Category);
        }

        public async Task<CategoryResult> GetCategoryByName(string Name)
        {
            var Category = await categoryRepository.GetCategoryByName(Name) ?? throw new NotFoundException("Category not found");
            return new CategoryResult(Category);
        }

        public async Task<CategoryResult> UpdateCategory(string Id, string Name, string Image)
        {
            if(await categoryRepository.GetCategoryById(Id) is null) throw new NotFoundException("Category not found");
            Category category = await categoryRepository.UpdateCategory(Id, Name, Image);

            return new CategoryResult(category);
        }
    }
}
