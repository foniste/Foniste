using FonApi.Database;
using FonApi.Interfaces;
using FonApi.Models.Exception;
using Microsoft.EntityFrameworkCore;

namespace FonApi.Service {
    public class VentureDbService : IVentureDbService {
        private readonly VenturesDbContext _venturesDbContext;

        //Exception model 
        public int exceptionId;
        public string exceptionMessage;
        public string exceptionDetail;
        //

        //Constructor
        public VentureDbService(VenturesDbContext venturesDbContext) {
            try
            {
                _venturesDbContext = venturesDbContext;

                this.exceptionId = 0;
                this.exceptionMessage = "Sorun Yok";
                this.exceptionDetail  = "Exception Yok.\n" +
                                   "Sorunsuz çalıştı.\n" +
                                   "ID: " + exceptionId;

                var excList = new List<dynamic>
                {
                    this.exceptionId,
                    this.exceptionMessage,
                    this.exceptionDetail
                };
                InsertWhenException(excList);
            }
            catch (DbUpdateException ex)
            {
                this.exceptionId = 1;
                this.exceptionMessage = ex.Message;
                this.exceptionDetail = "DbUpdateException\n" +
                                    "Veritabanı işleminde bir hata oluştu.\n" +
                                    "ID: " + exceptionId;
                var excList = new List<dynamic>
                {
                    this.exceptionId,
                    this.exceptionMessage,
                    this.exceptionDetail
                };
                InsertWhenException(excList);
            }
            catch (NotSupportedException ex)
            {
                this.exceptionId = 2;
                this.exceptionMessage = ex.Message;
                this.exceptionDetail = "InvalidOperationException\n" +
                                        "Desteklenmeyen bir işlem yapılmaya çalışıldı.\n" +
                                        "ID: " + exceptionId;

                var excList = new List<dynamic>
                {
                    this.exceptionId,
                    this.exceptionMessage,
                    this.exceptionDetail
                };
                InsertWhenException(excList);
            }
            catch (InvalidOperationException ex)
            {
                this.exceptionId = 3;
                this.exceptionMessage = ex.Message;
                this.exceptionDetail = "InvalidOperationException\n" +
                                        "Beklenmedik bir durum oluştu.\n" +
                                        "ID: " + exceptionId;

                var excList = new List<dynamic>
                {
                    this.exceptionId,
                    this.exceptionMessage,
                    this.exceptionDetail
                };
                InsertWhenException(excList);
            }
            catch (ArgumentNullException ex)
            {
                this.exceptionId = 4;
                this.exceptionMessage = "ArgumentNullException\n" +
                                        "Null bir argüman geçildi.\n" +
                                        "ID: " + exceptionId;
                this.exceptionDetail = ex.Message;

                var excList = new List<dynamic>
                {
                    this.exceptionId,
                    this.exceptionMessage,
                    this.exceptionDetail
                };
                InsertWhenException(excList);
            }
        }
        //

        //Constructor da exception yakaladığında exceptionlog tablosunda veri insert eden metod
        public void InsertWhenException(List<dynamic> excList)
        {
            var newexception = new ExceptionLog()
            {
                ExceptionId = excList[0],
                Message = excList[1],
                Detail = excList[2]
            };

            _venturesDbContext.exceptionlog.Add(newexception);
            _venturesDbContext.SaveChanges();
        }

        public int ControlDatabaseException()
        {
            return 0; // düzeltilcek
        }



        //Exception oluşup oluşmadığının kontrolü
        public bool IsThereException()
        {
            if(ControlDatabaseException() == 1 ||
               ControlDatabaseException() == 2 ||
               ControlDatabaseException() == 3 ||
               ControlDatabaseException() == 4)
            {
                return false;
            }
            return true;
        }
        //

        // Tüm ventureheader verilerini getiren metod
        public async Task<List<object>> GetAllHeaders()
        {
            if (!IsThereException())
            {
                var exceptionHolder = await _venturesDbContext.exceptionlog
                                                               .OrderByDescending(vh => vh.Id)
                                                               .FirstOrDefaultAsync();
                return new List<object> { exceptionHolder };
            }
            var holder = await _venturesDbContext.venturesheader.ToListAsync();
            return holder.Cast<object>().ToList();
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
