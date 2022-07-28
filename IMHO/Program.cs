using IMHO.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

//using Pomelo.EntityFrameworkCore.MySql;
//args represents command-line arguments
var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllersWithViews();
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
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
                var principal = context.Principal;
                if (principal.HasClaim(c => c.Type == ClaimTypes.NameIdentifier))
                {
                    if (principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value == "j")
                    {
                        var claimsIdentity = principal.Identity as ClaimsIdentity;
                        claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, "Admin"));
                    }
                }
                await Task.CompletedTask;
            },

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
        options.Authority = "https://accounts.google.com";
        options.ClientId = "467353587951-sv7r49l22iq10mscaupqdcnservlbmgv.apps.googleusercontent.com";
        options.ClientSecret = "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";
        options.CallbackPath = "/auth";
        options.SaveTokens = true;
        options.Events = new Microsoft.AspNetCore.Authentication.OpenIdConnect.OpenIdConnectEvents()
        {
            OnTokenValidated = async context =>
            {
                if (context.Principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value == "117945655236360512577")
                {
                    var claim = new Claim(ClaimTypes.Role, "Admin");
                    var claimsIdentity = context.Principal.Identity as ClaimsIdentity;
                    claimsIdentity.AddClaim(claim);

                }
            }
        };
    }).AddOpenIdConnect("okta", options =>
    {
        options.Authority = "https://dev-52978948.okta.com/oauth2/default";
        options.ClientId = "0oa5w45jmoXwz0buu5d7";
        options.ClientSecret = "77Bk6MI8azP7e_zMkhGoWZu9tcDUmyL31XhhRzjP";
        options.CallbackPath = "/okta-auth";
        options.ResponseType = OpenIdConnectResponseType.Code;
    });


//builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
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
