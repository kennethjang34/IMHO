
using Microsoft.AspNetCore.Mvc;
using IMHO.Data;
using System.Security.Claims;
using IMHO.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using IMHO.Services;
namespace IMHO.Controllers
{
    public class IMHOController<T> : Controller
    {
        //private readonly ILogger<AccountController> _logger;
        protected readonly ApplicationDbContext? _db;
        protected readonly UserService? _userService;
        protected readonly ILogger<T> _logger;
        public IMHOController(ApplicationDbContext db, UserService userService, ILogger<T> logger)
        {
            _db = db;
            _userService = userService;
            _logger = logger;
        }
    }
}
