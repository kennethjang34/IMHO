
using Microsoft.AspNetCore.Mvc;
using IMHO.Data;
using IMHO.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;

namespace IMHO.Controllers;
public class AccountController : Controller
{
    private readonly ApplicationDbContext _db;
    public AccountController(ApplicationDbContext db) { _db = db; }
    public IActionResult Index()
    {
        IEnumerable<Account> objAccountyList = _db.Accounts;
        return View(objAccountyList);
    }
    //return login page
    [HttpGet("login")]
    //[Route("login")]
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
        Account? foundWithTheId = _db.Accounts.Where(x => x.Id == account.Id).FirstOrDefault();

        if (foundWithTheId == null)
        {
            ModelState.AddModelError("Id", "The given ID already exists");
        }
        else if (ModelState.IsValid)
        {
            _db.Accounts.Add(account);
            _db.SaveChanges();
            TempData["success"] = $"Account: {account.Id} successfully created";

        }
        return View(account);
    }

    //GET
    //[HttpGet("sessions")]
    //[ValidateAntiForgeryToken]
    //public IActionResult Sessions()
    //{
    //return RedirectToAction("Login");
    //}
    //

    [HttpGet("sessions/{token?}")]
    //[ValidateAntiForgeryToken]
    public IActionResult Sessions(string? token)
    {
        if (token == null)
        {
            return RedirectToAction("Login");
        }
        else
        {
            return View();
        }
    }

    //This action logs the user in
    [HttpPost("sessions")]
    //[ValidateAntiForgeryToken]
    public async Task<IActionResult> Sessions(Account user, string returnUrl)
    {
        if (user.Id == "j" && user.Password == "j")
        {
            var claims = new List<Claim>();
            claims.Add(new Claim("Id", user.Id));
            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
            claims.Add(new Claim(ClaimTypes.Name, "Junhyeok"));
            claims.Add(new Claim(ClaimTypes.Role, "Admin"));
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            var items = new Dictionary<string, string?>();
            items.Add(".AuthScheme", CookieAuthenticationDefaults.AuthenticationScheme);
            var properties = new AuthenticationProperties(items);
            await HttpContext.SignInAsync(claimsPrincipal, properties);
            return Redirect(returnUrl);
        }
        TempData["Error"] = "Error. User ID or Password is not valid";
        return View("Login");
    }
    [HttpGet("denied")]
    public IActionResult Denied()
    {
        return View();
    }

    //[HttpDelete("sessions{token}")]
    //[Authorize]
    //public async Task<IActionResult> Logout()
    //{
    //var scheme = User.Claims.FirstOrDefault(c => c.Type == ".AuthScheme").Value;
    //if (scheme == "google")
    //{
    //await HttpContext.SignOutAsync();
    //return Redirect(@"https://www.google.com/accounts/Logout?continue=https://appengine.google.com/_ah/logout?continue=https://localhost:7089");
    //}
    //else { return new SignOutResult(new[] { CookieAuthenticationDefaults.AuthenticationScheme, scheme }); }
    //}
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
            //return new SignOutResult(new[] { CookieAuthenticationDefaults.AuthenticationScheme });
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

    [HttpPost("login")]
    public async Task<IActionResult> Validate(string username, string password, string returnUrl)
    {
        ViewData["returnUrl"] = returnUrl;
        if (username == "j" && password == "pizza")
        {
            var claims = new List<Claim>();
            claims.Add(new Claim("username", username));
            claims.Add(new Claim(ClaimTypes.NameIdentifier, username));
            claims.Add(new Claim(ClaimTypes.Name, username));
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            var items = new Dictionary<string, string?>();
            items.Add(".AuthScheme", CookieAuthenticationDefaults.AuthenticationScheme);
            var properties = new AuthenticationProperties(items);
            await HttpContext.SignInAsync(claimsPrincipal, properties);
            return Redirect(returnUrl);
        }
        TempData["Error"] = "Error. Username or Password is invalid";
        return View("login");
    }
}
