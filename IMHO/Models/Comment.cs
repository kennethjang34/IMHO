
using System.ComponentModel.DataAnnotations;
//using System.ComponentModel;
//using System.ComponentModel.DataAnnotations.Schema;

using System.ComponentModel.DataAnnotations.Schema;
namespace IMHO.Models
{
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }
        [Required]
        public int PostId { get; set; }
        [Required]
        public int AuthorId { get; set; }
        public Post Post { get; set; }
        public Account Author { get; set; }


    }


}
