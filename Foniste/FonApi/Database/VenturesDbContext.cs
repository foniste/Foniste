using FonApi.Models.Ventures;
using Microsoft.EntityFrameworkCore;

namespace FonApi.Database
{
    public class VenturesDbContext : DbContext {
        // Constructor
        public VenturesDbContext(DbContextOptions<VenturesDbContext> options) : base(options){
        }
        // Ventures Schema
        public DbSet<VenturesDetail> VenturesDetails{get;set;}
        public DbSet<VenturesHeader> VenturesHeader{get;set;}
        public DbSet<VenturesImg> VenturesImg{get;set;}
        //
    }

    
}
