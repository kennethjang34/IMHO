using IMHO.Models;
using IMHO.Data;
using System.Security.Claims;
namespace IMHO.Services
{
    public class UserService
    {
        private readonly ApplicationDbContext _context;
        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }


        internal Account? GetUserByExternalProvider(string provider, string nameIdentifier)
        {
            var account = _context.Accounts.Where(a => a.Provider == provider).Where(a => a.NameIdentifier == nameIdentifier).FirstOrDefault();
            //if (appUser is null)
            //{
            //throw new Exception("there is no user with such an identity");
            //}
            return account;
        }


        internal Account? GetUserById(int id)
        {
            var account = _context.Accounts.Find(id);
            //if (appUser is null)
            //{
            //throw new Exception("there is no user with such an identity");
            //}
            return account;
        }
        internal bool TryValidateUser(string username, string password, out List<Claim> claims)
        {
            claims = new List<Claim>();
            var account = _context.Accounts.Where(a => a.Username == username).Where(a => a.Password == password).FirstOrDefault();
            if (account is null)
            {
                return false;
            }
            else
            {
                claims.Add(new Claim(ClaimTypes.NameIdentifier, username));
                claims.Add(new Claim("username", username));
                claims.Add(new Claim(ClaimTypes.GivenName, account.FirstName));
                claims.Add(new Claim(ClaimTypes.Surname, account.LastName));
                claims.Add(new Claim(ClaimTypes.Email, account.Email));
                claims.Add(new Claim(ClaimTypes.MobilePhone, account.Mobile));
                foreach (var r in account.Roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, r));
                }
                return true;
            }
        }
        internal Account AddNewUser(string provider, List<Claim> claims)
        {
            var account = new Account();
            account.Provider = provider;
            account.NameIdentifier = claims.GetClaimString(ClaimTypes.NameIdentifier);
            account.Username = claims.GetClaimString("username");
            account.FirstName = claims.GetClaimString(ClaimTypes.GivenName);
            account.LastName = claims.GetClaimString(ClaimTypes.Surname);
            if (string.IsNullOrEmpty(account.FirstName))
            {
                account.FirstName = claims.GetClaimString(ClaimTypes.Name);
                foreach (var claim in claims)
                {
                    Console.WriteLine(claim.Type);
                }
            }
            account.RolesString = claims.GetClaimString(ClaimTypes.Role);
            account.Email = claims.GetClaimString(ClaimTypes.Email);
            account.Mobile = claims.GetClaimString(ClaimTypes.MobilePhone);
            var entity = _context.Accounts.Add(account);
            _context.SaveChanges();
            return entity.Entity;
        }
    }
    public static class Extensions
    {
        public static string? GetClaimString(this List<Claim> claims, string type)
        {
            var selected = claims.Where(c => c.Type == type).ToList();
            if (selected is null || selected.Count == 0)
            {
                return null;
            }
            else if (selected.Count > 1)
            {
                string res = "";
                for (int i = 0; i < selected.Count; i++)
                {
                    if (i != selected.Count - 1)
                    {
                        res += $"{selected[i].Value},";
                    }
                    else
                    {
                        res += selected[i].Value;
                    }
                }
                return res;
            }
            else
            {
                Console.WriteLine("TESTING");
                Console.WriteLine($"{type}: {selected[0].Value}");
                return selected[0].Value;
            }
            //return claims.FirstOrDefault(c => c.Type == type)?.Value;
        }
    }
}
