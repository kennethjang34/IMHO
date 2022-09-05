
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace IMHO.Models;
public class Channel
{
    public enum Accessibility
    {
        Private = 0,
        Protected = 1,
        Public = 2
    }
    [Key]
    public int ChannelId { get; set; }
    public string ChannelName { get; set; } = "test";
    public string Description { get; set; } = "";
    //M2M
    public List<Account> Members { get; set; } = new List<Account>();
    public Accessibility AccessibilityType { get; set; } = Accessibility.Public;
    //F.K One
    public List<Post> Posts
    { get; set; } = new List<Post>();
    //F.K 
    public List<Tag> Tags { get; set; } = new List<Tag>();
}

