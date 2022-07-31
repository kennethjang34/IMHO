using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using IMHO.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using IMHO.Services;


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
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }


    [Authorize(Roles = "Admin")]
    //    [Authorize]
    public async Task<IActionResult> Secured()
    {
        var idToken = await HttpContext.GetTokenAsync("id_token");
        return View();

    }






    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
