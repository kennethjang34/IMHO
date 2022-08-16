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
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Channel> Channels { get; set; }
        public DbSet<Comment> Comments { get; set; }

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
                e.Property(e => e.RolesString).HasMaxLength(1000);
                e.HasData(new Account
                {
                    Provider = "Cookies",
                    UserId = -1,
                    Email = "j@test.com",
                    Username = "j",
                    Password = "h",
                    FirstName = "Junhyeok",
                    LastName = "Jang",
                    Mobile = "111-111-111",
                    RolesString = "Admin"
                });
            });
            modelBuilder.Entity<Channel>(e =>
            {
                e.HasKey(e => e.ChannelId);
                e.Property(e => e.ChannelId);
                e.Property(e => e.Description);
                e.HasData(new Channel { ChannelId = -1, Description = "TEST CHANNEL" });
            });
            modelBuilder.Entity<Tag>(e =>
            {
                e.HasData(new Tag { TagId = -1, TagName = "TEST TAG", ChannelId = -1, TagDescription = "TEST TAG DESCRIPTION" });
            });
            modelBuilder.Entity<Tag>().HasOne(t => t.Channel).WithMany(c => c.Tags).HasForeignKey(t => t.ChannelId);
            modelBuilder.Entity<Post>(e =>
            {
                e.HasKey(e => e.PostId);
                e.Property(e => e.PostId);
                e.Property(e => e.AuthorId);
                e.Property(e => e.Title).HasMaxLength(150);
                e.Property(e => e.Views).HasMaxLength(100);
                e.Property(e => e.Body).HasMaxLength(500);
                e.Property(e => e.ExposedTo).HasMaxLength(10);
                e.Property(e => e.Published).HasMaxLength(10);
                e.Property(e => e.CreatedAt);
                e.Property(e => e.UpdatedAt);
                //e.Property(e => e.Image);
            });
            modelBuilder.Entity<Image>(e =>
            {
                e.HasKey(e => e.ImageId);
                e.Property(e => e.ImageName);
                e.Property(e => e.PostId);
                e.Property(e => e.Caption);
                e.Property(e => e.Uri).HasMaxLength(150);
                e.HasData(new Image { PostId = 2, ImageId = 2, Uri = "/Users/JANG/IMHO/IMHO/Resources/Images/seol.jpeg", Caption = "test image" });
            });
            modelBuilder.Entity<Image>().HasOne(i => i.Post).WithMany(p => p.Images).HasForeignKey(i => i.PostId);
            modelBuilder
            .Entity<Post>()
             .HasMany(p => p.Tags)
        .WithMany(t => t.Posts);
            modelBuilder.Entity<Account>().HasMany(a => a.Channels).WithMany(c => c.Members);
            modelBuilder.Entity<Post>().HasOne(p => p.Author).WithMany(a => a.Posts).HasForeignKey(p => p.AuthorId);
            modelBuilder.Entity<Post>().HasOne(p => p.Channel).WithMany(c => c.Posts).HasForeignKey(p => p.ChannelId);
            modelBuilder.Entity<Comment>().HasOne(c => c.Post).WithMany(p => p.Comments).HasForeignKey(c => c.CommentId);
            modelBuilder.Entity<Comment>().HasOne(c => c.Author).WithMany(a => a.Comments).HasForeignKey(c => c.AuthorId);
            //!! ef core foreign key with alternate key ex:
            //ModelBuilder.Entity<Post>().HasOne(p=>p.Channel).WithMany(c=>c.Posts).HasForeignKey(p=>p.ChannelURl).HasPrincipalKey(c=>c.ChannelURl);
            //Enum value conversion is automatically configured by EF Core. The following code is for an example of value conversion.
            //modelBuilder.Entity<Post>().Property(e => e.ExposedTo).HasConversion(v => v.ToString(), v => (Post.ExposedLevel)Enum.Parse(typeof(Post.ExposedLevel), v));
        }
    }
}
