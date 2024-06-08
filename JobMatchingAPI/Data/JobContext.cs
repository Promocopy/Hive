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
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Member>()
                .HasKey(m => m.EMAIL);  // Configure primary key explicitly if needed
            modelBuilder.Entity<MemberEducarionalHistory>()
                .HasKey(m => m.DegreeClassification);
            modelBuilder.Entity<MemberWorkHistory>()
                .HasKey(m => m.Industry);
        }

        public DbSet<User> User { get; set; }
        public DbSet<Member> Member { get; set; }
        public DbSet<MemberEducarionalHistory> EHistories { get; set; }
        public DbSet<MemberWorkHistory> WorkHistories { get; set; }
        
    }



}