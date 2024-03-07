using FonApi.Models.Accounts;
using FonApi.Models.Ventures;
using Microsoft.EntityFrameworkCore;

namespace FonApi.Database
{
    public class AccountsDbContext : DbContext {
        // Constructor
        public AccountsDbContext(DbContextOptions<AccountsDbContext> options) : base(options){
        }
        //
        
        // Accounts Schema
        public DbSet<LoginLog> login_log{get;set;}
        public DbSet<Organizations> organization{get;set;}
        public DbSet<Role> role{get;set;}
        public DbSet<UserAuth> user_auth{get;set;}
        public DbSet<Users> users{get;set;}
        //
    }

    
}
