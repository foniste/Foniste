using FonApi.Database;
using FonApi.Interfaces;
using FonApi.Models.Ventures;
using Microsoft.EntityFrameworkCore;

namespace FonApi.Service {
    public class VentureDbService : IVentureDbService {
        private readonly VenturesDbContext _venturesDbContext;

        //Constructor
        public VentureDbService(VenturesDbContext venturesDbContext) {
            _venturesDbContext = venturesDbContext ?? throw new ArgumentNullException(nameof(venturesDbContext)); 
            //_venturesDbContext boş gelmesi durumunda exception
        }
        //

        // Tüm ventureheader verilerini getiren metod
        public async Task<List<VenturesHeader>> GetAllHeaders()
        {
            var holder = _venturesDbContext.venturesheader.ToListAsync();
            if(holder == null || holder.Result.Count == 0)
            {

                return new List<VenturesHeader> { new VenturesHeader(){
                    VentureHeader = "deneme",
                    VentureId = 1,
                    HeaderThumbnail = "deneme/path",
                    CreationDate = DateTime.Now,
                    TargetFund = 1000 } 
                }; 
            }
            //_venturesDbContext.Dispose();
            return await holder;
        }
        //

        //// Tüm venturedetails verilerini getiren metod
        //public async Task<List<VenturesDetail>> GetAllDetails()
        //{
        //    return await _venturesDbContext.VenturesDetails.ToListAsync();
        //}
        ////

        //// Yeni bir girişim ilanı eklendiğinde header tablosuna insert eden metod
        //public void InsertNewVentureHeader(VenturesHeader venturesHeader)
        //{
        //    try
        //    {
        //        _venturesDbContext.VenturesHeader.Add(venturesHeader);
        //        _venturesDbContext.SaveChanges();
        //    }
        //    catch (System.Exception)
        //    {
        //        throw;
        //    }
        //}
        ////


        //// Yeni bir ilan açıldığında ilan detaylarını tabloya insert eden metod
        //public void InsertNewVentureDetails(VenturesDetail venturesDetail)
        //{
        //    try
        //    {
        //        _venturesDbContext.VenturesDetails.Add(venturesDetail);
        //        _venturesDbContext.SaveChanges();
        //    }
        //    catch (System.Exception)
        //    {
        //        throw;
        //    }
        //}
        ////



    }
}
