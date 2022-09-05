using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace IMHO.Models
{
    public class Account
    {
        public int UserId { get; set; }
        public string? Provider { get; set; }
        public string? NameIdentifier { get; set; }
        public string Username { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Mobile { get; set; }
        [Display(Name = "Roles")]
        [Column("Roles")]
        public string? RolesString { get; set; }
        //F.K
        public List<Post> Posts { get; set; } = new List<Post>();
        //F.K
        public List<Comment> Comments { get; set; } = new List<Comment>();
        //M2M
        public List<Channel> Channels { get; set; } = new List<Channel>();
        [NotMapped]
        public List<string> Roles
        {
            get { return RolesString?.Split(',').ToList() ?? new List<string>(); }
        }

    }
}
