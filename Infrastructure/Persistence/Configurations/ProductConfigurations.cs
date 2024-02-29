using Domain.CategoryAggregate;
using Domain.CategoryAggregate.ValueObjects;
using Domain.ProductAggregate;
using Domain.ProductAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Configurations
{
    public class ProductConfigurations : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            ConfigureProductBuilder(builder);
        }

        private void ConfigureProductBuilder(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");
            //builder.HasKey(p => p.id);

            builder.Property(p => p.Id)
             .IsRequired()
             .HasConversion(
                 id => id.value,
                 value => ProductId.Create(value)
             );

            builder.ComplexProperty(p => p.Price);

            builder.ComplexProperty(p => p.Name);

            builder.Property(p => p.Description);
            builder.Property(p => p.Images);
        }
    }
}
