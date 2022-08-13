using IMHO.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using IMHO.Services;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Google.Apis.Auth;
using System.IdentityModel.Tokens.Jwt;
namespace IMHO.Services
{
    public class GoogleTokenValidator : ISecurityTokenValidator
    {

        private readonly JwtSecurityTokenHandler _tokenHandler;
        public GoogleTokenValidator()
        {
            _tokenHandler = new JwtSecurityTokenHandler();
        }

        public bool CanValidateToken => true;
        public int MaximumTokenSizeInBytes { get; set; } = TokenValidationParameters.DefaultMaximumTokenSizeInBytes;

        public bool CanReadToken(string securityToken)
        {
            //Console.WriteLine(_tokenHandler.CanReadToken(securityToken));
            //Console.WriteLine(securityToken);
            return _tokenHandler.CanReadToken(securityToken);
        }


        public ClaimsPrincipal ValidateToken(string securityToken, TokenValidationParameters validationParameters, out SecurityToken validatedToken)
        {
            //Console.WriteLine("ValidateToken Called..?");
            validatedToken = _tokenHandler.ReadJwtToken(securityToken);
            var payload = GoogleJsonWebSignature.ValidateAsync(securityToken, new GoogleJsonWebSignature.ValidationSettings()).Result;
            Console.WriteLine($"Google ID token name identifier: {payload.Name}");
            var claims = new List<Claim>{
            new Claim(ClaimTypes.NameIdentifier,payload.Subject),
            new Claim(ClaimTypes.Name,payload.Name),
            new Claim(JwtRegisteredClaimNames.FamilyName,payload.FamilyName),
            new Claim(JwtRegisteredClaimNames.GivenName, payload.GivenName),
            new Claim(JwtRegisteredClaimNames.Email, payload.Email),
        new Claim(JwtRegisteredClaimNames.Sub, payload.Subject),
            new Claim(JwtRegisteredClaimNames.Iss, payload.Issuer)
	    };
            try
            {
                var principal = new ClaimsPrincipal();
                principal.AddIdentity(new ClaimsIdentity(claims, JwtBearerDefaults.AuthenticationScheme));
                //Console.WriteLine("Seems alright though..");
                return principal;
            }
            catch (Exception e)
            {
                //Console.WriteLine("error happend");
                Console.WriteLine(e);
                throw;
            }
        }
    }



}
