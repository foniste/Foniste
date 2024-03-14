using FonApi.Models.Accounts;
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

        public int exceptionId = 0;
        public string exceptionMessage = string.Empty;
        public string exceptionDetail = string.Empty;

        public int ControlDatabaseException()
        {
            try
            {
                this.exceptionMessage = "Exception Yok.\n" +
                                        "Sorunsuz çalýþtý.\n" +
                                        "ID: " + exceptionId;
                this.exceptionDetail = "Sorun Yok";
                return this.exceptionId;
            }
            catch (DbUpdateException ex)
            {
                this.exceptionId = 1;
                this.exceptionMessage = "DbUpdateException\n" +
                                        "Veritabaný iþleminde bir hata oluþtu.\n" + 
                                        "ID: " + exceptionId;
                this.exceptionDetail = ex.Message;
                return this.exceptionId;
            }
            catch (NotSupportedException ex)
            {
                this.exceptionId = 2;
                this.exceptionMessage = "InvalidOperationException\n" +
                                        "Desteklenmeyen bir iþlem yapýlmaya çalýþýldý.\n" +
                                        "ID: " + exceptionId;
                this.exceptionDetail= ex.Message;
                return this.exceptionId;
            }
            catch (InvalidOperationException ex)
            {   
                this.exceptionId = 3;
                this.exceptionMessage = "InvalidOperationException\n" +
                                        "Beklenmedik bir durum oluþtu.\n" +
                                        "ID: " + exceptionId;
                this.exceptionDetail = ex.Message;
                return this.exceptionId;
            }
            catch (ArgumentNullException ex)
            {
                this.exceptionId = 4;
                this.exceptionMessage = "ArgumentNullException\n" +
                                        "Null bir argüman geçildi.\n" +
                                        "ID: " + exceptionId;
                this.exceptionDetail = ex.Message;
                return this.exceptionId;
            }
        }

        
    }

    

    
}
