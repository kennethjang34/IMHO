
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
    //public string? Uri { get; set; }
    public string? Format { get; set; }
    public string? FileName { get; set; }
    [Timestamp]
    public DateTime UploadedAt { get; set; } = DateTime.Now;


    public string? GetFileName()
    {
        return FileName;
    }
    public string GetFullFileName()
    {
        return FileName + Format;
    }

}
