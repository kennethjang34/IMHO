
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
namespace IMHO.Models;
public class Image
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    [Display(Name = "Image ID")]
    public int? ImageId { get; set; }
    public int? PostId { get; set; }
    public string? ImageName { get; set; }
    public Post? Post { get; set; }
    public string? Caption { get; set; }
    public string? Uri { get; set; }
}
