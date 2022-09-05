using Microsoft.AspNetCore.Mvc;
using IMHO.Data;
using IMHO.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using IMHO.Services;
//using System.Data.Entity;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Net.Http.Headers;
using System.Net;
using System.Net.Mime;
using Microsoft.EntityFrameworkCore;
namespace IMHO.Controllers
{
    public class ChannelController
    : IMHOController<ChannelController>


    {
        private readonly int _defaultPostcount = 10;
        public ChannelController(ApplicationDbContext db, UserService userService, ILogger<ChannelController> logger)
        : base(db, userService, logger)
        {
        }
        [HttpGet("channels/{channelId:int}/posts")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Posts(int channelId, [FromQuery(Name = "user-id")] int? userId, [FromQuery(Name = "limit")] int? limit, [FromQuery(Name = "offset")] int? offset)
        {
            //var userService = HttpContext.RequestServices.GetRequiredService(typeof(UserService)) as UserService;
            //var identity = User.Identity as ClaimsIdentity;
            //var nameIdentifier = identity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            //var account = userService.GetUserByExternalProvider("google", nameIdentifier, (a) => a.Channels);
            Account account = this.getAccount()!;
            Channel? channel = _db.Channels.Include((ch) => ch.Posts).ThenInclude((p) => p.Images).FirstOrDefault((ch) => ch.ChannelId == channelId);
            if (channel == null)
            {
                return NotFound();
            }
            else
            {
                return Json(channel.Posts.OrderByDescending((p) => p.UpdatedAt).Skip(offset ?? 0).Take(limit ?? this._defaultPostcount));
            }
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("channels/{channelName}-{channelId:int}/memberships")]
        //[HttpPost("channels/{channelNameId}/memberships")]
        public async Task<IActionResult> JoinChannel(string channelName, int channelId)
        {
            //int channelId = Int32.Parse(channelNameId.Split('-')[1]);
            Account account = this.getAccount()!;
            Console.WriteLine($"channel name parsed: {channelName}, channel id: {channelId}");
            Channel? channel = _db!.Channels.FirstOrDefault((ch) => ch.ChannelId == channelId);
            if (channel == null)
            {
                return NotFound();
            }
            else if (channel.AccessibilityType == Channel.Accessibility.Public)
            {
                channel.Members.Add(account);
                return Ok();
            }
            return Unauthorized();
        }



    }
}
