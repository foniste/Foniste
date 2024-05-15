using FonApi.Models.Ventures;
using Microsoft.EntityFrameworkCore;

namespace FonApi.Database
{
    public class VenturesDbContext : DbContext{
        // Constructor
        public VenturesDbContext(DbContextOptions<VenturesDbContext> options) : base(options) {
        }
        // Ventures Schema
        public DbSet<VenturesAll> ventures_all { get; set;}
        //
    }
}
