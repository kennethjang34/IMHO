

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
    public class TagController : IMHOController<TagController>
    {
        public TagController(ApplicationDbContext db, UserService userService, ILogger<TagController> logger)
        : base(db, userService, logger)
        {
        }
    }
}
