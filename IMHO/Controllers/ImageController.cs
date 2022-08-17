
using Microsoft.AspNetCore.Mvc;
using IMHO.Data;
using IMHO.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using IMHO.Services;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Net.Http.Headers;
using System.Net;
using System.Net.Mime;
namespace IMHO.Controllers
{
    public class ImageController
    : IMHOController<ImageController>
    {
        public ImageController(ApplicationDbContext db, UserService userService, ILogger<ImageController> logger)
        : base(db, userService, logger)
        {
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("Images/{imageId}")]
        public async Task<IActionResult> GetImage(int imageId)
        {
            if (User != null && User.Identities.Any(identity => identity.IsAuthenticated))
            {
                var identity1 = User.Identities.FirstOrDefault(i => i.IsAuthenticated) as ClaimsIdentity;
            }
            else
            {
                Console.WriteLine("no user");
            }
            var userService = HttpContext.RequestServices.GetRequiredService(typeof(UserService)) as UserService;
            var identity = User.Identity as ClaimsIdentity;
            var nameIdentifier = identity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var author = userService.GetUserByExternalProvider("google", nameIdentifier);
            string? storagePath = null;
            Console.WriteLine("Image controller");
            var image = _db.Images.FirstOrDefault(img => img.ImageId == imageId);
            var folderName = Path.Combine("Resources", "Images");
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), folderName);
            var fullFileName = image.GetFullFileName();
            var fullPath = Path.Combine(folderPath, fullFileName);
            bool result = System.IO.File.Exists(fullPath);
            var img = System.IO.File.OpenRead(fullPath);
            return File(img, $"image/{image.Format}");
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("Images")]
        public async Task<IActionResult> GetImages([FromQuery(Name = "post-id")] int postId)
        {
            if (User != null && User.Identities.Any(identity => identity.IsAuthenticated))
            {
                var identity1 = User.Identities.FirstOrDefault(i => i.IsAuthenticated) as ClaimsIdentity;
            }
            else
            {
                Console.WriteLine("no user");
            }
            var userService = HttpContext.RequestServices.GetRequiredService(typeof(UserService)) as UserService;
            var identity = User.Identity as ClaimsIdentity;
            var nameIdentifier = identity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var author = userService.GetUserByExternalProvider("google", nameIdentifier);
            string? storagePath = null;
            Console.WriteLine("Get images with query parameter");
            var images = _db.Images.Where(img => img.PostId == postId);
            return Json(images);
        }

    }
}
