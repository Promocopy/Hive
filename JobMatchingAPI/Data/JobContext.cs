using JobMatchingAPI.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace JobMatchingAPI.Data
{
    public class JobContext : IdentityDbContext<User>
    {
        public JobContext(DbContextOptions options) : base(options)
        {
        }

        private void SeedRole(ModelBuilder builder)
        {
            builder.Entity<IdentityRole>().HasData
                (
                new IdentityRole() { Name = "Admin", ConcurrencyStamp = "1", NormalizedName = "Admin" },
                new IdentityRole() { Name = "User", ConcurrencyStamp = "2", NormalizedName = "User" }
                );
        }

        public DbSet<User> User { get; set; }

    }
}
