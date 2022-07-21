using Microsoft.EntityFrameworkCore;
using IMHO.Models;
//using MySqlConnector;
namespace IMHO.Data
{
    public class ApplicationDbContext : DbContext
    {




        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

	}
        public DbSet<Account> Accounts { get; set; }
//	public DbSet<Test> Abc{get;set;}
    }



}
