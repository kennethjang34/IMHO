using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
namespace IMHO.Models;
public class Post
{
    public int PostId { get; set; }
    //public int AuthorId { get; set; }
    [Required]
    [Display(Name = "Author ID")]
    public Account Author { get; set; }
    [Required]
    public int AuthorId { get; set; }
    public string? Title { get; set; }
    public string? Channel { get; set; }
    public string? Tags { get; set; }
    public string? Body { get; set; }
    public string? Image { get; set; }
    public string? ExposedTo { get; set; }
    public bool Published { get; set; } = false;
    public int Views { get; set; } = 0;
    [Timestamp]
    public DateTime? UpdatedAt { get; set; }
    [Required]
    [Timestamp]
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public List<string> TagList
    {
        get { return Tags?.Split(',').ToList() ?? new List<string>(); }
    }
}


