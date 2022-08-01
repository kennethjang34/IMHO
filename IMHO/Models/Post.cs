
namespace IMHO.Models;
public class Post
{
    public int AuthorId { get; set; }
    public string? Title { get; set; }
    public string? Channel { get; set; }
    public string? Tags { get; set; }
    public List<string> TagList
    {
        get { return Tags?.Split(',').ToList() ?? new List<string>(); }
    }
}
