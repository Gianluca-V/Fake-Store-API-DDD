using Application.Common.Interfaces.Persistence;
using Domain.CategoryAggregate;
using Domain.ProductAggregate.ValueObjects;
using Domain.ProductAggregate;
using Domain.UserAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using Domain.CategoryAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly FakeStoreAPIDbContext dbContext;

        public CategoryRepository(FakeStoreAPIDbContext DbContext)
        {
            dbContext = DbContext;
        }

        public virtual async Task CreateCategory(Category category)
        {
            await dbContext.Categories.AddAsync(category);
            await dbContext.SaveChangesAsync();
        }

        public virtual async Task<List<Category>> GetCategories()
        {
            return await dbContext.Categories.ToListAsync();        //when a field has null data throws an error
        }

        public virtual async Task<Category?> GetCategoryById(string Id)
        {
            CategoryId categoryId;
            try
            {
                categoryId = CategoryId.Create(Guid.Parse(Id));
            }
            catch (Exception)
            {
                return null;
            }
            return await dbContext.Categories.FindAsync(categoryId);
        }

        public virtual async Task<Category?> GetCategoryByName(string Name)
        {
            return await dbContext.Categories.Where(c => c.Name.value == Name).FirstOrDefaultAsync();
        }

        public virtual async Task<Category?> UpdateCategory(string Id, string Name, string Image)
        {
            Category? category = await GetCategoryById(Id);
            if (category is null) return category;
            category.Name = CategoryName.CreateName(Name);
            category.Image = Image;
            await dbContext.SaveChangesAsync();
            return category;
        }

        public virtual async Task<bool> DeleteCategory(string Name)
        {
            try
            {
                var category = await GetCategoryByName(Name);
                if (category is null) return false; 
                dbContext.Categories.Remove(category);
                await dbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
