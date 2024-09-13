using Microsoft.EntityFrameworkCore;
using Shopping.Services.ProductAPI.Enumerators;
using Shopping.Services.ProductAPI.Models;

namespace Shopping.Services.ProductAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating (ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId = 1,
                Name = "Produto1",
                Price = 15,
                Description = "This is the description for Product 1. It is an excelent Appetizer!!!",
                ImageUrl = "https://placehold.co/603x403",
                CategoryName = Category.Appetizer
            });

			modelBuilder.Entity<Product>().HasData(new Product
			{
				ProductId = 2,
				Name = "Produto2",
				Price = 13.99,
				Description = "This is the description for Product 2. It is an excelent Appetizer!!!",
				ImageUrl = "https://placehold.co/602x402",
				CategoryName = Category.Appetizer
			});

			modelBuilder.Entity<Product>().HasData(new Product
			{
				ProductId = 3,
				Name = "Produto3",
				Price = 10.99,
				Description = "This is the description for Product 3. It is an excelent Dessert!!!",
				ImageUrl = "https://placehold.co/601x401",
				CategoryName = Category.Dessert
			});

			modelBuilder.Entity<Product>().HasData(new Product
			{
				ProductId = 4,
				Name = "Produto4",
				Price = 15,
				Description = "This is the description for Product 4. It is an excelent Entree!!!",
				ImageUrl = "https://placehold.co/600x400",
				CategoryName = Category.Entree
			});
		}
    }
}
