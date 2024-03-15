using Domain.CategoryAggregate;
using Domain.ProductAggregate;
using Domain.UserAggregate;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence
{
    public class FakeStoreAPIDbContext : DbContext
    {
        public FakeStoreAPIDbContext(DbContextOptions<FakeStoreAPIDbContext> options) : base(options)
        {
        }
        public FakeStoreAPIDbContext() { }

        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<Category> Categories { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(FakeStoreAPIDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
