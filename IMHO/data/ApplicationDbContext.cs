using Microsoft.EntityFrameworkCore;
using IMHO.Models;
//using MySqlConnector;
namespace IMHO.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Account> Accounts { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.HasKey(e => e.UserId);
                entity.Property(e => e.UserId);
                entity.Property(e => e.Provider).HasMaxLength(250);
                entity.Property(e => e.NameIdentifier).HasMaxLength(500);
                entity.Property(e => e.Username).HasMaxLength(250);
                entity.Property(e => e.Password).HasMaxLength(250);
                entity.Property(e => e.Email).HasMaxLength(250);
                entity.Property(e => e.FirstName).HasMaxLength(250);
                entity.Property(e => e.LastName).HasMaxLength(250);
                entity.Property(e => e.Mobile).HasMaxLength(250);
                entity.Property(e => e.Roles).HasMaxLength(1000);
                entity.HasData(new Account
                {
                    Provider = "Cookies",
                    UserId = 1,
                    Email = "j@test.com",
                    Username = "j",
                    Password = "h",
                    FirstName = "Junhyeok",
                    LastName = "Jang",
                    Mobile = "111-111-111",
                    Roles = "Admin"
                });

            });
        }
    }



}
