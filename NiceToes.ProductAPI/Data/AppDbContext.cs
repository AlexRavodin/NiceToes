using Microsoft.EntityFrameworkCore;
using NiceToes.ProductAPI.Models;

namespace NiceToes.ProductAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId = 1,
                Name = "Boots",
                Price = 400,
                Description = "For cold walks.",
                ImageUrl = "https://localhost:7000/Images/1.jpg",
                CategoryName = "Winter"
            });
            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId = 2,
                Name = "Shoes",
                Price = 450,
                Description = "For business meetings.",
                ImageUrl = "https://localhost:7000/Images/2.jpg",
                CategoryName = "Winter"
            });
            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId = 3,
                Name = "Trainers",
                Price = 300,
                Description = "For doing sports.",
                ImageUrl = "https://localhost:7000/Images/3.jpg",
                CategoryName = "Summer"
            });
            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId = 4,
                Name = "Ballet shoes",
                Price = 100,
                Description = "For dancing.",
                ImageUrl = "https://localhost:7000/Images/4.jpg",
                CategoryName = "Scene"
            });
        }
    }
}
