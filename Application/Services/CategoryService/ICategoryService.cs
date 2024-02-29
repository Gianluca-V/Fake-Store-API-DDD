using Application.Services.ProductService;
using Domain.CategoryAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.CategoryService
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryResult>> GetCategories();
        Task<CategoryResult> GetCategoryById(string Id);
        Task<CategoryResult> GetCategoryByName(string Name);
        Task<CategoryResult> CreateCategory(string Name, string Image);
        Task<CategoryResult> UpdateCategory(string Id, string Name, string Image);
        Task DeleteCategory(string CategoryId);
    }
}
