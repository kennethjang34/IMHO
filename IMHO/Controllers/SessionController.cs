
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
    public class SessionController : IMHOController<SessionController>
    {
        public SessionController(ApplicationDbContext db, UserService userService, ILogger<SessionController> logger)
        : base(db, userService, logger)
        {
        }
        [HttpGet]
        public IActionResult Sessions(Account account)
        {
            if (account != null)
            {
                return Redirect($"sessions/{account.Username}");
            }
            else
            {

                return RedirectToAction("Login", "AccountController");
            }
        }



        //[HttpGet("sessions/{username}")]
        ////[ValidateAntiForgeryToken]
        //public IActionResult UserSession(Account account)
        //{
        //if (account == null)
        //{
        //return RedirectToAction("Login", "AccountController");
        //}
        //else
        //{
        //return View();
        //}
        //}
        //[Authorize(Roles = "Admin")]
        //[Authorize]
        //public IActionResult NewPost(Account account)
        //{
        //ViewData["UserId"] = account.UserId;
        //ViewData["Username"] = account.Username;
        //ViewData["ChannelId"] = -1;
        //return View("~/Views/Post/NewPost.cshtml");
        //}
    }




}
