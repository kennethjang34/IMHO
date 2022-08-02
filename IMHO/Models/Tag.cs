
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
namespace IMHO.Models
{
    public class Tag
    {
        [Key]
        public int TagId { get; set; }
        public string? TagName { get; set; }
        public string TagDescription { get; set; } = "";
        //F.K Many
        public Channel Channel { get; set; }
        public int ChannelId { get; set; }
        public List<Post> Posts { get; set; } = new List<Post>();



    }




}
