using FonApi.Database;
using FonApi.Interfaces;
using FonApi.Models.Exception;
using Microsoft.EntityFrameworkCore;

namespace FonApi.Service {
    public class VentureDbService : IVentureDbService{
        private readonly VenturesDbContext _venturesDbContext;

        public int exceptionId { get; set; }
        public string exceptionMessage { get; set; }
        public string exceptionDetail { get; set; }

        // Constructor
        public VentureDbService(VenturesDbContext venturesDbContext) {
            try
            {
                _venturesDbContext = venturesDbContext;

                this.exceptionId = 0;
                this.exceptionMessage = "Sorun Yok";
                this.exceptionDetail  = "Hata Yok. Sorunsuz çalıştı. Veri çekmeye çalıştığınız tabloda veri bulunmamaktadır. ID: " + exceptionId;

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
                this.exceptionDetail = "DbUpdateException oluştu .Veritabanı işleminde bir hata oluştu. ID: " + exceptionId;
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
                this.exceptionDetail = "InvalidOperationException oluştu. Desteklenmeyen bir işlem yapılmaya çalışıldı. ID: " + exceptionId;

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
                this.exceptionDetail = "InvalidOperationException oluştu. Beklenmedik bir durum oluştu. ID: " + exceptionId;

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
                this.exceptionMessage = "ArgumentNullException oluştu. Null bir argüman geçildi. ID: " + exceptionId;
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

        // Constructor da exception yakaladığında exceptionlog tablosunda veri insert eden metod
        public void InsertWhenException(List<dynamic> excList)
        {
            var newexception = new ExceptionLog()
            {
                ExceptionId = excList[0],
                Message = excList[1],
                Detail = excList[2]
            };

            try
            {
                _venturesDbContext.exceptionlog.Add(newexception);
                _venturesDbContext.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                // Veritabanı güncelleme hatası
                Console.WriteLine("Veritabanı güncelleme hatası: " + ex.Message);
            }
            catch (Exception ex)
            {
                // Diğer hatalar
                Console.WriteLine("Beklenmeyen bir hata oluştu: " + ex.Message);
            }
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

            if(holder == null || holder.Count == 0) {
                var exceptionHolder = await _venturesDbContext.exceptionlog
                                                              .OrderByDescending(vh => vh.Id)
                                                              .FirstOrDefaultAsync();
                return new List<object> { exceptionHolder };
            }


            return holder.Cast<object>().ToList();
        }
        //



        // REVİZE EDİLECEK //
        // Exception oluşup oluşmadığının kontrolü
        public bool IsThereException()
        {
            if (ControlDatabaseException() == 1 ||
               ControlDatabaseException() == 2 ||
               ControlDatabaseException() == 3 ||
               ControlDatabaseException() == 4)
            {
                return false;
            }
            return true;
        }
        //

        public int ControlDatabaseException()
        {
            return 0; 
        }
        //

        // revize edilecek //
    }
}
