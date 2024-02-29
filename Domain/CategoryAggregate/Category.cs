using Domain.CategoryAggregate.ValueObjects;
using Domain.Common.Models;
using Domain.ProductAggregate;
using Domain.ProductAggregate.ValueObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Domain.CategoryAggregate
{
    public class Category : AggregateRoot<CategoryId>
    {
     /* [Key]
        public CategoryId Id { get; set; } */
        public CategoryName Name { get; set; }
        public string? Image { get; set; }
        public ICollection<Product> Products { get; set; }

        private Category() {}

        public static Category Create(string Name, string Image)
        {
            var c = new Category
            {
                Id = CategoryId.CreateUnique(),
                Name = CategoryName.CreateName(Name),
                Image = Image
            };
            return c;
        }
    }
}
