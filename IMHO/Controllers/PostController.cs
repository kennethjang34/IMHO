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
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost]
        //[Authorize]
        public async Task<IActionResult> NewPost([FromBody] IDictionary<string, string> jsonDict)
        {

            if (User != null && User.Identities.Any(identity => identity.IsAuthenticated))
            {
                var identity1 = User.Identities.FirstOrDefault(i => i.IsAuthenticated) as ClaimsIdentity;

                Console.WriteLine("Well user authenticated at least");
                //Console.WriteLine($"{identity1.Claims.FirstOrDefault((c => { return c.Type == ClaimTypes.NameIdentifier; }))}");
                foreach (Claim c in identity1.Claims)
                {
                    Console.WriteLine($"Key: {c.Type}, Value: {c.Value}");
                }
                //RedirectToAction("", "Home");
            }
            else
            {
                Console.WriteLine("no user");
            }
            //string returnUrl = string.IsNullOrEmpty(returnUrl) ? "/" : returnUrl;
            //var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            //var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            //var items = new Dictionary<string, string?>();
            //items.Add(".AuthScheme", CookieAuthenticationDefaults.AuthenticationScheme);
            //var authenticationProperties = new AuthenticationProperties { RedirectUri = returnUrl };
            //await HttpContext.SignInAsync(JwtBearerDefaults.AuthenticationScheme, properties);
            //await HttpContext.ChallengeAsync(provider, authenticationProperties).ConfigureAwait(false);
            //return new ChallengeResult(provider, authenticationProperties);

            var userService = HttpContext.RequestServices.GetRequiredService(typeof(UserService)) as UserService;
            var identity = User.Identity as ClaimsIdentity;
            Console.WriteLine($"Identity given name: {identity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)}");
            var nameIdentifier = identity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            //var scheme = identity.Claims.FirstOrDefault(c => c.Type == ".AuthScheme");
            //Assuming the user logged in through idaas provider
            var author = userService.GetUserByExternalProvider("google", nameIdentifier);
            string title = jsonDict["Title"];
            int tagId = Int32.Parse(jsonDict["TagId"]);
            int channelId = Int32.Parse(jsonDict["ChannelId"]);
            string body = jsonDict["Body"];
            Console.WriteLine(author.UserId);
            //Console.WriteLine($"ChannelID Given: {channelId}");
            var tag = _db.Tags.FirstOrDefault(t => t.TagId == tagId);
            Post post = new Post { AuthorId = author.UserId, Title = title, Body = body, ChannelId = channelId, Tags = new List<Tag> { tag } };
            if (TryValidateModel(post))
            {
                _db.Posts?.Add(post);
                _db.SaveChanges();
                TempData["success"] = "The new post successfully created";
                Console.WriteLine("Successful");
                return RedirectToAction("Index");
            }
            else
            {
                //Console.WriteLine($"Error{author.FirstName}");
                TempData["error"] = "Error occurred while creating the post";
                return View("~/Views/Home/NewPost.cshtml");
                //return View("PostInfo", post);
                //return BadRequest();
            }
        }
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
