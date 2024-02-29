using Domain.CategoryAggregate;
using Domain.ProductAggregate;
using Domain.ProductAggregate.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.Persistence
{
    public interface IProductRepository
    {
        Task<List<Product>> GetProducts();
        Task<Product?> GetProductById(string Id);
        Task CreateProduct(Product product);
        Task<Product> UpdateProduct(string Id, string Name, string Description, float Price, Category Category, List<string> Images);
        Task<bool> DeleteProduct(string Id);
    }
}
