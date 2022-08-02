
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace IMHO.Models;
public class Channel
{
    [Key]
    public int ChannelId { get; set; }
    public string Description { get; set; } = "";
    //M2M
    public List<Account> Members { get; set; } = new List<Account>();
    //F.K One
    public List<Post> Posts { get; set; } = new List<Post>();
    //F.K 
    public List<Tag> Tags { get; set; } = new List<Tag>();
}

