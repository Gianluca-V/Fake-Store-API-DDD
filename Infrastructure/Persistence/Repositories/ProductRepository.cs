using Application.Common.Interfaces.Persistence;
using Domain.CategoryAggregate;
using Domain.Common.Models;
using Domain.ProductAggregate;
using Domain.ProductAggregate.ValueObjects;
using Domain.UserAggregate;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly FakeStoreAPIDbContext dbContext;

        public ProductRepository(FakeStoreAPIDbContext DbContext)
        {
            dbContext = DbContext;
        }
        public ProductRepository(){}

        public virtual async Task CreateProduct(Product product)
        {
            await dbContext.Products.AddAsync(product);
            await dbContext.SaveChangesAsync();
        }

        public virtual async Task<Product?> GetProductById(string Id)
        {
            ProductId productId;
            try
            {
                productId = ProductId.Create(Guid.Parse(Id));
            }
            catch (Exception)
            {
                return null;
            }
            return await dbContext.Products
                .Where(p => p.Id == productId)
                .Include(p => p.Category)
                .FirstOrDefaultAsync();
        }

        public virtual async Task<List<Product>> GetProducts()
        {
            return await dbContext.Products.Include(p => p.Category).ToListAsync();
        }

        public virtual async Task<List<Product>> GetProducts(int page, int pageSize)
        {
            return await dbContext.Products
                .Include(p => p.Category)
                .Skip((page-1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public virtual async Task<Product?> UpdateProduct(string Id, string Name, string Description, float Price, Category Category, List<string> Images)
        {
            Product? product = await GetProductById(Id);
            if (product is null) return product;
            product.Name = ProductName.CreateName(Name);
            product.Description = Description;
            product.Price = ProductPrice.CreatePrice(Price);
            product.CategoryId = Category.Id;
            product.Category = Category;
            product.Images = Images;
            await dbContext.SaveChangesAsync();
            return product;
        }

        public virtual async Task<bool> DeleteProduct(string Id)
        {
            try
            {
                var product = await GetProductById(Id);
                dbContext.Products.Remove(product);
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
