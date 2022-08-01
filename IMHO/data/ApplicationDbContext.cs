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
        public DbSet<Post> Posts { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(e =>
            {
                e.HasKey(e => e.UserId);
                e.Property(e => e.UserId);
                e.Property(e => e.Provider).HasMaxLength(250);
                e.Property(e => e.NameIdentifier).HasMaxLength(500);
                e.Property(e => e.Username).HasMaxLength(250);
                e.Property(e => e.Password).HasMaxLength(250);
                e.Property(e => e.Email).HasMaxLength(250);
                e.Property(e => e.FirstName).HasMaxLength(250);
                e.Property(e => e.LastName).HasMaxLength(250);
                e.Property(e => e.Mobile).HasMaxLength(250);
                e.Property(e => e.Roles).HasMaxLength(1000);
                e.HasData(new Account
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
            modelBuilder.Entity<Post>(e =>
            {
                e.HasKey(e => e.PostId);
                e.Property(e => e.PostId);
                e.Property(e => e.AuthorId);
                e.Property(e => e.Title).HasMaxLength(150);
                //e.Property(e => e.Author);
                e.Property(e => e.Tags);
                e.Property(e => e.Views).HasMaxLength(100);
                e.Property(e => e.Body).HasMaxLength(500);
                e.Property(e => e.ExposedTo).HasMaxLength(10);
                e.Property(e => e.Published).HasMaxLength(10);
                e.Property(e => e.CreatedAt);
                e.Property(e => e.UpdatedAt);
            });
            modelBuilder.Entity<Post>().HasOne(p => p.Author).WithMany(a => a.Posts).HasForeignKey(p => p.AuthorId);
        }
    }



}
