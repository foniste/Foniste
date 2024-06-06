using FonApi.Database;
using FonApi.Models.Ventures;
using Microsoft.EntityFrameworkCore;

namespace FonApi.Service
{
    public class VentureDbService
    {
        private readonly VenturesDbContext _ventureDbContext;

        public VentureDbService(VenturesDbContext ventureDbContext,AccountsDbContext accountsDbContext)
        {
            _ventureDbContext = ventureDbContext ?? throw new ArgumentNullException(nameof(ventureDbContext));
        }

        public async Task<List<VenturesAll>> GetAll() {
            return await _ventureDbContext.ventures_all.ToListAsync();
        }
        

        public async Task<List<VenturesAll>> GetVenturesByOrganizationIdAsync(int organizationId)
        {
            return await _ventureDbContext.ventures_all
                                 .Where(v => v.organization_id == organizationId)
                                 .ToListAsync();
        }



        public void InsertNewVenture(VenturesAll ventures_all)
        {
            _ventureDbContext.ventures_all.Add(ventures_all);
        }


        // Veritabanındaki DML sorgularının değişikliklerinin sağlanması için api de yazdığın fonskiyona bunu ekle try bloğunda çalıştır.
        public void Initialize()
        {
            _ventureDbContext.SaveChanges();
        }
        //
    }
}
