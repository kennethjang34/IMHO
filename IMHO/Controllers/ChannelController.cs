using Microsoft.AspNetCore.Mvc;
using IMHO.Data;
using IMHO.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using IMHO.Services;
//using System.Data.Entity;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Net.Http.Headers;
using System.Net;
using System.Net.Mime;
using Microsoft.EntityFrameworkCore;
namespace IMHO.Controllers
{
    public class ChannelController
    : IMHOController<ChannelController>


    {
        private readonly int _defaultPostcount = 10;
        public ChannelController(ApplicationDbContext db, UserService userService, ILogger<ChannelController> logger)
        : base(db, userService, logger)
        {
        }
        [HttpGet("channels/{channelId:int}/posts")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Posts(int channelId, [FromQuery(Name = "user-id")] int? userId, [FromQuery(Name = "limit")] int? limit, [FromQuery(Name = "offset")] int? offset)
        {
            var userService = HttpContext.RequestServices.GetRequiredService(typeof(UserService)) as UserService;
            var identity = User.Identity as ClaimsIdentity;
            var nameIdentifier = identity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var account = userService.GetUserByExternalProvider("google", nameIdentifier);
            Channel? channel = _db.Channels.Include((ch) => ch.Posts).ThenInclude((p) => p.Images).FirstOrDefault((ch) => ch.ChannelId == channelId);
            if (channel == null)
            {
                return NotFound();
            }
            else
            {
                return Json(channel.Posts.OrderByDescending((p) => p.UpdatedAt).Skip(offset ?? 0).Take(limit ?? this._defaultPostcount));
            }
        }


        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost]
        //[Authorize]
        public async Task<IActionResult> NewPost(IFormCollection collection, IList<IFormFile> images)
        {
            var userService = HttpContext.RequestServices.GetRequiredService(typeof(UserService)) as UserService;
            var identity = User.Identity as ClaimsIdentity;
            var nameIdentifier = identity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var author = userService.GetUserByExternalProvider("google", nameIdentifier);
            foreach (var c in collection)
            {
                Console.WriteLine(c.ToString());
            }
            string? storagePath = null;
            string title = collection["Title"];
            int tagId = Int32.Parse(collection["TagId"]);
            int channelId = Int32.Parse(collection["ChannelId"]);
            string body = collection["Body"];
            var tag = _db.Tags.FirstOrDefault(t => t.TagId == tagId);
            Post post = new Post { AuthorId = author.UserId, Title = title, Body = body, ChannelId = channelId, Tags = new List<Tag> { tag }, Images = new List<Image>() };
            Console.WriteLine($"Image count: {images.Count()}");
            if (images.Count() > 0)
            {
                for (int i = 0; i < images.Count(); i++)
                {
                    IFormFile imageFile = images[i];
                    var folderName = Path.Combine("Resources", "Images");
                    var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                    //Image name != File name. File name must be unique, needs to be determined before saving to db
                    var fullImageName = ContentDispositionHeaderValue.Parse(imageFile.ContentDisposition).FileName.Trim('"');
                    var fullFileName = string.Format(@"{0}{1}", Guid.NewGuid(), Path.GetExtension(fullImageName));
                    var caption = collection["ImageCaptions"][i];
                    Image image = new Image
                    {
                        ImageName = Path.GetFileNameWithoutExtension(fullImageName),
                        FileName = Path.GetFileNameWithoutExtension(fullFileName),
                        Format = Path.GetExtension(fullImageName),
                        Post = post,
                        Caption = caption
                    };
                    var fullPath = Path.Combine(pathToSave, fullFileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        imageFile.CopyTo(stream);
                    }
                    post.Images.Add(image);
                }
            }
            //Console.WriteLine(image);
            if (TryValidateModel(post))
            {
                _db.Posts?.Add(post);
                _db.SaveChanges();
                TempData["success"] = "The new post successfully created";
                Console.WriteLine("Successful");
                return Json(post);
            }
            else
            {
                var errors = ModelState.Select(x => x.Value.Errors)
                              .Where(y => y.Count > 0)
                              .ToList();
                foreach (var e in errors[0])
                {
                    Console.WriteLine(e.ErrorMessage);
                }
                TempData["error"] = "Error occurred while creating the post";
                Console.WriteLine("Not Successful");
                return View("~/Views/Home/NewPost.cshtml");
            }
        }

    }
}
