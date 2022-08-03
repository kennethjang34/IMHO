using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using IMHO.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using IMHO.Services;
using System.Security.Claims;

namespace IMHO.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly UserService? _userService;

    //public HomeController(ILogger<HomeController> logger)
    //{
    //_logger = logger;
    //}
    public HomeController(ILogger<HomeController> logger, UserService userService)
    {
        _logger = logger;
        _userService = userService;
    }
    public IActionResult Index()
    {
        if (User.Identity.IsAuthenticated)
        {
            //Console.WriteLine(User);
            return View("~/Views/Home/UserHome.cshtml");
        }
        else
        {
            return View();
        }
    }
    public IActionResult Privacy()
    {
        return View();
    }
    //[Authorize(Roles = "Admin")]
    //[Authorize]
    //public IActionResult NewPost()
    //{
    //var idToken = await HttpContext.GetTokenAsync("id_token");
    //return View();
    //}
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
    [Authorize]
    [HttpGet]
    public IActionResult NewPost()
    {
        ViewData["Name"] = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
        ViewData["NameIdentifier"] = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        ViewData["ChannelId"] = -1;
        return View();
    }
}
