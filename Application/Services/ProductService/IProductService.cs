using Domain.ProductAggregate;
using Domain.ProductAggregate.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.ProductService
{
    public interface IProductService
    {
        Task<IEnumerable<ProductResult>> GetProducts();
        Task<ProductResult> GetProduct(string productId);
        Task<ProductResult> CreateProduct(string Name, string Description, float Price, string CategoryId, List<string> Images);
        Task<ProductResult> UpdateProduct(string Id ,string Name, string Description, float Price, string CategoryId, List<string> Images);
        Task DeleteProduct(string productId);
    }
}
