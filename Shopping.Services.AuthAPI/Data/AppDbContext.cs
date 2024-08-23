using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Shopping.Services.AuthAPI.Models;

namespace Shopping.Services.AuthAPI.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        protected override void OnModelCreating (ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

			//Make prop "FullName" the 2nd column (after column "Id"):
			//modelBuilder.Entity<ApplicationUser>(entity =>
			//{
			//	entity.Property(e => e.FullName).HasColumnOrder(2);
			//});
		}
	}
}
