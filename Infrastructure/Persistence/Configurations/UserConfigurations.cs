using Domain.ProductAggregate;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.UserAggregate;
using Domain.UserAggregate.ValueObjects;

namespace Infrastructure.Persistence.Configurations
{
    public class UserConfigurations : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            ConfigureUserBuilder(builder);
        }

        private void ConfigureUserBuilder(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            //builder.HasKey(p => p.id);

            builder.Property(u => u.Id)
             .IsRequired()
             .HasConversion(
                 id => id.value,
                 value => UserId.Create(value)
             );

            builder.ComplexProperty(u => u.Email);

            builder.Property(u => u.Username);
            builder.Property(u => u.Password);
        }
    }
}
