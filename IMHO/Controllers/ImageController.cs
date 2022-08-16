
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
        //[Authorize]
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
            //string? storagePath = null;
            var image = _db.Images.FirstOrDefault(img => img.ImageId == imageId);
            var dir = Directory.GetCurrentDirectory();
            var path = Path.Combine(dir, image.FileName);
            return base.File(path, "image/jpeg");
        }



        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        //[HttpPost]
        ////[Authorize]
        //public async Task<IActionResult> NewPost([FromBody] IDictionary<string, string> dict)
        //{

        //if (User != null && User.Identities.Any(identity => identity.IsAuthenticated))
        //{
        //var identity1 = User.Identities.FirstOrDefault(i => i.IsAuthenticated) as ClaimsIdentity;

        //foreach (Claim c in identity1.Claims)
        //{
        //Console.WriteLine($"Key: {c.Type}, Value: {c.Value}");
        //}
        //}
        //else
        //{
        //Console.WriteLine("no user");
        //}
        //var userService = HttpContext.RequestServices.GetRequiredService(typeof(UserService)) as UserService;
        //var identity = User.Identity as ClaimsIdentity;
        //Console.WriteLine($"Identity given name: {identity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)}");
        //var nameIdentifier = identity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        //var author = userService.GetUserByExternalProvider("google", nameIdentifier);
        //string title = dict["Title"];
        //int tagId = Int32.Parse(dict["TagId"]);
        //int channelId = Int32.Parse(dict["ChannelId"]);
        //string body = dict["Body"];
        //Console.WriteLine(author.UserId);
        //var tag = _db.Tags.FirstOrDefault(t => t.TagId == tagId);
        //Post post = new Post { AuthorId = author.UserId, Title = title, Body = body, ChannelId = channelId, Tags = new List<Tag> { tag } };
        //if (TryValidateModel(post))
        //{
        //_db.Posts?.Add(post);
        //_db.SaveChanges();
        //TempData["success"] = "The new post successfully created";
        //Console.WriteLine("Successful");
        //return Json(post);
        ////return RedirectToAction("Index");
        //}
        //else
        //{
        //TempData["error"] = "Error occurred while creating the post";
        //return View("~/Views/Home/NewPost.cshtml");
        //}
        //}
        //[HttpPost]
        //public async Task<IActionResult> NewPost([FromBody] IDictionary<string, string> jsonDict)
        //{
        //string title = jsonDict["Title"];
        //string tagId = jsonDict["TagId"];
        //string channelId = jsonDict["ChannelId"];
        //var userService = HttpContext.RequestServices.GetRequiredService(typeof(UserService)) as UserService;
        //var identity = User.Identity as ClaimsIdentity;
        //var nameIdentifier = identity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        //var scheme = identity.Claims.FirstOrDefault(c => c.Type == ".AuthScheme");
        ////Assuming the user logged in through idaas provider
        //var author = userService.GetUserByExternalProvider(scheme.Value, nameIdentifier);
        //Console.WriteLine($"tag id given: { tagId }");
        //Console.WriteLine($"ChannelID Given: {channelId}");
        //var tag = _db.Tags.FirstOrDefault(t => t.TagId == tagId);
        //Post post = new Post { AuthorId = author.UserId, Title = title, Body = body, ChannelId = channelId, Tags = new List<Tag> { tag } };
        //if (TryValidateModel(post))
        //{
        //_db.Posts?.Add(post);
        //_db.SaveChanges();
        //TempData["success"] = "The new post successfully created";
        //return RedirectToAction("Index");
        //}
        //else
        //{
        //Console.WriteLine($"Error{author.FirstName}");
        //TempData["error"] = "Error occurred while creating the post";
        //return View("~/Views/Home/NewPost.cshtml");
        ////return View("PostInfo", post);

        ////return BadRequest();
        //}
        //}
    }
}
