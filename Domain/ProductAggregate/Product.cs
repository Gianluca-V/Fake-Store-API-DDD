using Domain.CategoryAggregate;
using Domain.CategoryAggregate.ValueObjects;
using Domain.Common.Models;
using Domain.ProductAggregate.ValueObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ProductAggregate
{
    public class Product : AggregateRoot<ProductId>
    {
        /*[Key]
        public ProductId Id { get; set; }*/
        public ProductName Name { get; set; } 
        public string? Description { get; set; }
        public ProductPrice Price { get; set; }
        public Category Category { get; set; }
        public CategoryId CategoryId { get; set; }
        public List<string>? Images { get; set; }

        private Product() { }

        public static Product Create(string Name, string Description, float Price, Category Category, List<string> Images)
        {
            return new Product {
                Id = ProductId.CreateUnique(),
                Name = ProductName.CreateName(Name),
                Description = Description,
                Price = ProductPrice.CreatePrice(Price),
                Category = Category,
                CategoryId = Category.Id,
                Images = Images
            };
        }
    }
}
