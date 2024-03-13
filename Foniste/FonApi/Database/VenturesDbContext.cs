using FonApi.Models.Ventures;
using Microsoft.EntityFrameworkCore;

namespace FonApi.Database
{
    public class VenturesDbContext : DbContext {
        // Constructor
        public VenturesDbContext(DbContextOptions<VenturesDbContext> options) : base(options){
        }
        // Ventures Schema
        public DbSet<VenturesDetail> venturesdetails{get;set;}
        public DbSet<VenturesHeader> venturesheader{get;set;}
        public DbSet<VenturesImg> venturesimg{get;set;}
        //
    }

    
}
