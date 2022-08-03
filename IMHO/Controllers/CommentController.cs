
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
    public class CommentController
    : IMHOController<CommentController>
    {
        public CommentController(ApplicationDbContext db, UserService userService, ILogger<CommentController> logger)
        : base(db, userService, logger)
        {
        }
    }
}
