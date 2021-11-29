using Microsoft.EntityFrameworkCore;
using TestProject.WebAPI.Repository.Models;

namespace TestProject.WebAPI.Db
{
    public class ZipProjectDbContext: DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Account> Accounts { get; set; }

        public ZipProjectDbContext(DbContextOptions<ZipProjectDbContext> options) : base(options)
        {
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<User>().HasKey(x => x.Email);

            modelBuilder.Entity<Account>().ToTable("Accounts");
            modelBuilder.Entity<Account>().HasKey(x => x.Email);

        }
    }
}