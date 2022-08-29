using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
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
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    [Display(Name = "Post ID")]
    public int? PostId { get; set; }
    [JsonIgnore]
    public Account? Author { get; set; }
    [Required]
    public int AuthorId { get; set; }
    public string? Title { get; set; }
    //F.K
    public int ChannelId { get; set; }
    public Channel? Channel { get; set; }
    //[Display(Name = "Tags")]
    //public string? TagString { get; set; }
    public string? Body { get; set; }
    //public string? Image { get; set; }
    public IList<Image> Images { get; set; } = new List<Image>();
    public ExposedLevel? ExposedTo { get; set; } = ExposedLevel.Public;
    public bool Published { get; set; } = false;
    public int Views { get; set; } = 0;
    public IList<Comment> Comments { get; set; } = new List<Comment>();
    [Timestamp]
    public DateTime? UpdatedAt { get; set; }
    [Timestamp]
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public IList<Tag> Tags { get; set; } = new List<Tag>();
    //[NotMapped]
    //public List<string> TagList
    //{
    //get { return TagString?.Split(',').ToList() ?? new List<string>(); }
    //}
}
