using IMHO.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using IMHO.Services;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
//using Pomelo.EntityFrameworkCore.MySql;
//args represents command-line arguments
var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllersWithViews();
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
builder.Services.AddScoped<UserService>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    //options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;

}).AddCookie(
    options =>
    {
        options.LoginPath = "/login";
        options.AccessDeniedPath = "/denied";

        options.Events = new CookieAuthenticationEvents()
        {
            OnSigningIn = async context =>
            {
                var scheme = context.Properties.Items.Where(k => k.Key == ".AuthScheme").FirstOrDefault();
                var claim = new Claim(scheme.Key, scheme.Value);
                var claimsIdentity = context.Principal.Identity as ClaimsIdentity;
                var userService = context.HttpContext.RequestServices.GetRequiredService(typeof(UserService)) as UserService;
                var nameIdentifier = claimsIdentity.Claims.FirstOrDefault(m => m.Type == ClaimTypes.NameIdentifier)?.Value;
                if (userService != null && nameIdentifier != null)
                {
                    var account = userService.GetUserByExternalProvider(scheme.Value, nameIdentifier);
                    //a new user joined
                    if (account is null)
                    {
                        account = userService.AddNewUser(scheme.Value, claimsIdentity.Claims.ToList());
                    }
                    foreach (var role in account.RoleList)
                    {
                        if (!claimsIdentity.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToList().Contains(role))
                        {
                            claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, role));
                        }
                    }
                }
                claimsIdentity.AddClaim(claim);
            },
            //OnSigningIn = async context =>
            //{
            //var principal = context.Principal;
            //if (principal.HasClaim(c => c.Type == ClaimTypes.NameIdentifier))
            //{
            //if (principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value == "j")
            //{
            //var claimsIdentity = principal.Identity as ClaimsIdentity;
            //claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, "Admin"));
            //}
            //}
            //await Task.CompletedTask;
            //},

            OnSignedIn = async context => { await Task.CompletedTask; },

            OnValidatePrincipal = async context => { await Task.CompletedTask; }
        };
    })
//.AddGoogle(options =>
//{
////Put the following in the user secrets file later
//options.ClientId = "467353587951-sv7r49l22iq10mscaupqdcnservlbmgv.apps.googleusercontent.com";
//options.ClientSecret = "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";
//options.CallbackPath = "/auth";
//options.AuthorizationEndpoint += "?prompt=consent";
//})
.AddOpenIdConnect("google", options =>
    {
        options.Authority = builder.Configuration["GoogleOpenId:Authority"];
        options.ClientId = builder.Configuration["GoogleOpenId:ClientId"];
        options.ClientSecret = builder.Configuration["GoogleOpenId:ClientSecret"];
        options.CallbackPath = builder.Configuration["GoogleOpenId:CallbackPath"];
        options.SignedOutCallbackPath = builder.Configuration["GoogleOpenId:SignedOutCallbackPath"];

        //options.Authority = "https://accounts.google.com";
        //options.ClientId = "467353587951-sv7r49l22iq10mscaupqdcnservlbmgv.apps.googleusercontent.com";
        //options.ClientSecret = "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";
        //options.CallbackPath = "/auth";
        //options.SignedOutCallbackPath = "google-signout";
        options.SaveTokens = true;
    }).AddOpenIdConnect("okta", options =>
    {
        options.Authority = builder.Configuration["Okta:Authority"];
        options.ClientId = builder.Configuration["Okta:ClientId"];
        options.ClientSecret = builder.Configuration["Okta:ClientSecret"];
        options.CallbackPath = builder.Configuration["Okta:CallbackPath"];
        options.SignedOutCallbackPath = builder.Configuration["Okta:SignedOutCallbackPath"];
        options.SaveTokens = true;

        //options.Authority = "https://dev-52978948.okta.com/oauth2/default";
        //options.ClientId = "0oa5w45jmoXwz0buu5d7";
        //options.ClientSecret = "77Bk6MI8azP7e_zMkhGoWZu9tcDUmyL31XhhRzjP";
        //options.CallbackPath = "/okta-auth";
        //options.SignedOutCallbackPath = "/okta-signout";
        options.ResponseType = OpenIdConnectResponseType.Code;
        options.SaveTokens = true;
        options.Scope.Add("openid");
        options.Scope.Add("profile");
        options.Scope.Add("offline_access");
        options.Events = new OpenIdConnectEvents()
        {
            OnRedirectToIdentityProvider = async (context) =>
            {
                var redirectUri = context.ProtocolMessage.RedirectUri;
                await Task.CompletedTask;
            }
        };
    });
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
//builder.Services.AddServerSideBlazor();
var app = builder.Build();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllerRoute(
name: "default",
pattern: "{controller=Home}/{action=Index}/{id?}");
app.Run();
