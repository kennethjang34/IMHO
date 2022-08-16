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
    public class PostController
    : IMHOController<PostController>
    {
        public PostController(ApplicationDbContext db, UserService userService, ILogger<PostController> logger)
        : base(db, userService, logger)
        {
        }

        //[HttpPost]
        //public async Task<IActionResult> NewPost(Post post)
        //{
        //if (ModelState.IsValid)
        //{
        //_db.Posts.Add(post);
        //_db.SaveChanges();
        //TempData["success"] = "The new post successfully created";
        //return RedirectToAction("Index");
        //}
        //else
        //{
        //return View("PostInfo", post);
        ////return BadRequest();
        //}
        //}
        //[HttpPost]
        //public async Task<IActionResult> NewPost([FromBody] IDictionary<string, string> jsonDict)
        //{
        ////string jsonString = Request.GetBody();
        //Console.WriteLine(jsonDict.ToString());
        //Console.WriteLine(jsonDict["Title"]);
        //return RedirectToAction("Index");
        //}
        //Currentlty only support google id token (JWT format)


        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost]
        //[Authorize]
        public async Task<IActionResult> NewPost(IFormCollection collection, IList<IFormFile> images)
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
            //foreach (var c in collection)
            //{
            //Console.WriteLine(c.ToString());
            //}
            //var file = Request.Form.Files[0];
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
                    storagePath = Path.Combine(folderName, fullImageName);
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
                //return Ok(Json(post));
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
