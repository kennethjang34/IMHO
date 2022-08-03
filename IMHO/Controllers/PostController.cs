using Microsoft.AspNetCore.Mvc;
using IMHO.Data;
using IMHO.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using IMHO.Services;
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
        [HttpPost]
        public async Task<IActionResult> NewPost(string title, string body, int tagId, int channelId, bool published = false)
        {
            var userService = HttpContext.RequestServices.GetRequiredService(typeof(UserService)) as UserService;
            var identity = User.Identity as ClaimsIdentity;
            var nameIdentifier = identity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var scheme = identity.Claims.FirstOrDefault(c => c.Type == ".AuthScheme");
            //Assuming the user logged in through idaas provider
            var author = userService.GetUserByExternalProvider(scheme.Value, nameIdentifier);
            //var tag = _db.Tags.FirstOrDefault(t => t.TagId == tagId);
            Post post = new Post { AuthorId = author.UserId, Title = title, Body = body, ChannelId = channelId };
            if (TryValidateModel(post))
            {
                _db.Posts?.Add(post);
                _db.SaveChanges();
                TempData["success"] = "The new post successfully created";
                return RedirectToAction("Index");
            }
            else
            {
                Console.WriteLine($"Error{author.FirstName}");
                TempData["error"] = "Error occurred while creating the post";
                return View("~/Views/Home/NewPost.cshtml");
                //return View("PostInfo", post);
                //return BadRequest();
            }
        }
    }
}
