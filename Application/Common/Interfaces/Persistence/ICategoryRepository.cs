using Domain.CategoryAggregate;
using Domain.ProductAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.Persistence
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetCategories();
        Task<Category?> GetCategoryById(string Id);
        Task<Category?> GetCategoryByName(string Name);
        Task CreateCategory(Category Category);
        Task<Category> UpdateCategory(string Id, string Name, string Image);
        Task<bool> DeleteCategory(string Name);
    }
}
