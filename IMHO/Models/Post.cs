using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
namespace IMHO.Models;
public class Post
{
    public enum ExposedLevel
    {
        Myself,
        Selected,
        Friends,
        Public
    }
    //public int AuthorId { get; set; }
    [Display(Name = "Post ID")]
    public int PostId { get; set; }
    [Required]
    [Display(Name = "Author ID")]
    public Account Author { get; set; }
    [Required]
    public int AuthorId { get; set; }
    public string? Title { get; set; }
    //F.K
    public int ChannelId { get; set; }
    [Required]
    public Channel Channel { get; set; }
    [Display(Name = "Tags")]
    public string? TagString { get; set; }
    public string? Body { get; set; }
    public string? Image { get; set; }
    [Required]
    public ExposedLevel? ExposedTo { get; set; } = ExposedLevel.Public;
    [Required]
    public bool Published { get; set; } = false;
    [Required]
    public int Views { get; set; } = 0;
    public List<Comment> Comments { get; set; } = new List<Comment>();
    [Timestamp]
    public DateTime? UpdatedAt { get; set; }
    [Required]
    [Timestamp]
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public List<Tag> Tags { get; set; } = new List<Tag>();
    [NotMapped]
    public List<string> TagList
    {
        get { return TagString?.Split(',').ToList() ?? new List<string>(); }
    }
}
