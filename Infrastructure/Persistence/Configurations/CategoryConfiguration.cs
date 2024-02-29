using Domain.CategoryAggregate;
using Domain.CategoryAggregate.ValueObjects;
using Domain.ProductAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            ConfigureCategoryBuilder(builder);
        }

        private void ConfigureCategoryBuilder(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Categories");
            //builder.HasKey(c => c.id);

            builder.Property(c => c.Id)
              .IsRequired()
              .HasConversion(
                  id => id.value,
                  value => CategoryId.Create(value)
              );


            builder.ComplexProperty(c => c.Name)
                .IsRequired();

            builder.Property(c => c.Image);

            builder.HasMany(c => c.Products)
            .WithOne(p => p.Category)
            .HasForeignKey(p => p.CategoryId);
        }
    }
}
