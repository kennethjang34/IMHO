
using Microsoft.AspNetCore.Mvc;
using IMHO.Data;
using IMHO.Models;
using System.Security.Principal;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

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






    [HttpPost("sessions")]
    //[ValidateAntiForgeryToken]
    public async Task<IActionResult> Sessions(Account user, string returnUrl)
    {
        if (user.Id == "j" && user.Password == "j")
        {
            var claims = new List<Claim>();
            claims.Add(new Claim("Id", user.Id));
            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

            return Redirect(returnUrl);
            //return RedirectToAction($"{user.Id}");
        }
        return View();
    }

}




//GET
//public IActionResult Create()
//{
//return View();
//}


//example reference
////POST
//[HttpPost]
//[ValidateAntiForgeryToken]
//public IActionResult Create(Category obj)
//{
//if (obj.Name == obj.DisplayOrder.ToString()) { ModelState.AddModelError("Name", "The DisplayOrder cannot exactly match the Name."); }
//if (ModelState.IsValid)
//{
//_db.Categories.Add(obj);
//_db.SaveChanges();
//TempData["success"] = "Category created succesfully";
//return RedirectToAction("Index");
//}
//return View(obj);
//}


////GET
//public IActionResult Edit(int? id)
//{
//if (id == null || id == 0)
//{
//return NotFound();
//}

//var categoryFromDb = _db.Categories.Find(id);
////var categoryFromDbFirst = _db.Categories.FirstOrDefault(u => u.Id == id);
////var categoryFromDbSingle = _db.Categories.SingleOrDefault(u => u.Id == id);

//if (categoryFromDb == null)
//{
//return NotFound();
//}
//return View(categoryFromDb);
//}

//[HttpPost]
//[ValidateAntiForgeryToken]
//public IActionResult Edit(Category obj)
//{
//if (obj.Name == obj.DisplayOrder.ToString())
//{
//ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the Name");

//}
//if (ModelState.IsValid)
//{
//_db.Categories.Update(obj);

//_db.SaveChanges();
//TempData["success"] = "Category updated succesfully";
//return RedirectToAction("Index");
//}
//return View(obj);
//}
//public IActionResult Delete(int? id)
//{
//if (id == null || id == 0)
//{
//return NotFound();
//}
//var categoryFromDb = _db.Categories.Find(id);

//if (categoryFromDb == null)
//{
//return NotFound();
//}
//return View(categoryFromDb);
//}



//[HttpPost, ActionName("Delete")]
//[ValidateAntiForgeryToken]
//public IActionResult DeletePost(int? id)
//{
//var obj = _db.Categories.Find(id);
//if (obj == null)
//{
//return NotFound();
//}
//_db.Categories.Remove(obj);
//_db.SaveChanges();
//TempData["success"] = "Category deleted succesfully";
//return RedirectToAction("Index");
//}
//}
