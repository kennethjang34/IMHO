using Microsoft.AspNetCore.Mvc;
using IMHO.Data;
using IMHO.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using IMHO.Services;
namespace IMHO.Controllers;
public class AccountController : Controller
{
    private readonly ApplicationDbContext _db;
    private readonly UserService _userService;
    private readonly ILogger<AccountController> _logger;
    public AccountController(ApplicationDbContext db, UserService userService, ILogger<AccountController> logger)
    {
        _db = db;
        _userService = userService;
        _logger = logger;
    }
    [HttpPost("sessions")]
    //[Route("validate")]
    public async Task<IActionResult> Sessions(string username, string password, string returnUrl)
    {
        returnUrl ??= "/";
        ViewData["ReturnUrl"] = returnUrl;
        if (_userService.TryValidateUser(username, password, out List<Claim> claims))
        {
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            var items = new Dictionary<string, string?>();
            items.Add(".AuthScheme", CookieAuthenticationDefaults.AuthenticationScheme);
            var properties = new AuthenticationProperties(items);
            await HttpContext.SignInAsync(claimsPrincipal, properties);
            return Redirect(returnUrl);
        }
        else
        {
            TempData["Error"] = "Error. Username or Password is invalid";
            return View("login");
        }
    }
    public IActionResult Index()
    {
        IEnumerable<Account> objAccountyList = _db.Accounts;
        return View(objAccountyList);
    }

    [Authorize]
    [HttpGet("oidc-api/user-profile")]
    public IActionResult GetUserData()
    {
        var userService = HttpContext.RequestServices.GetRequiredService(typeof(UserService)) as UserService;
        ClaimsIdentity identity = User.Identity as ClaimsIdentity ?? throw new Exception("User.Identity is null or cannot be converted to ClaimsIdentity");
        string nameIdentifier = identity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)!.Value ?? throw new Exception("NameIdentifier value cannot be null");
        var author = _userService.GetUserByExternalProvider("google", nameIdentifier);
        Console.WriteLine($"Author: {author}");
        return Ok(author);
    }
    //return login page
    [HttpGet("login")]
    //[ValidateAntiForgeryToken]
    public IActionResult Login(string returnUrl)
    {
        ViewData["ReturnUrl"] = returnUrl;
        return View();
    }
    //GET
    [HttpGet]
    //[ValidateAntiForgeryToken]
    public IActionResult Users()
    {
        return View();
    }
    [HttpPost("users")]
    //[ValidateAntiForgeryToken]
    public IActionResult Users(Account account)
    {
        Account? foundWithTheId = _db.Accounts.Where(x => x.Username == account.Username).FirstOrDefault();

        if (foundWithTheId == null)
        {
            ModelState.AddModelError("Username", "The given ID already exists");
        }
        else if (ModelState.IsValid)
        {
            _db.Accounts.Add(account);
            _db.SaveChanges();
            TempData["success"] = $"Account: {account.Username} successfully created";
        }
        return View(account);
    }
    [HttpGet("denied")]
    public IActionResult Denied()
    {
        return View();
    }
    [Authorize]
    public async Task<IActionResult> Logout()
    {
        var scheme = User.Claims.FirstOrDefault(c => c.Type == ".AuthScheme").Value;
        if (scheme == "google")
        {
            await HttpContext.SignOutAsync();
            return Redirect(@"https://www.google.com/accounts/Logout?continue=https://appengine.google.com/_ah/logout?continue=https://localhost:7089");
        }
        else
        {
            var redirectUri = "/";
            var properties = new AuthenticationProperties();
            properties.RedirectUri = redirectUri;
            return new SignOutResult(new[] { CookieAuthenticationDefaults.AuthenticationScheme, scheme }, properties);
        }
    }
    [HttpGet("login/{provider}")]
    public IActionResult LoginExternal([FromRoute] string provider, [FromQuery] string returnUrl)
    {
        if (User != null && User.Identities.Any(identity => identity.IsAuthenticated))
        {
            RedirectToAction("", "Home");
        }
        returnUrl = string.IsNullOrEmpty(returnUrl) ? "/" : returnUrl;
        var authenticationProperties = new AuthenticationProperties { RedirectUri = returnUrl };
        //await HttpContext.ChallengeAsync(provider, authenticationProperties).ConfigureAwait(false);
        return new ChallengeResult(provider, authenticationProperties);
    }
}
