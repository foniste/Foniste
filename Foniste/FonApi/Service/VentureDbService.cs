using FonApi.Database;
using FonApi.Interfaces;
using FonApi.Models.Exception;
using FonApi.Models.Ventures;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace FonApi.Service {
    public class VentureDbService : IVentureDbService{
        private readonly VenturesDbContext _venturesDbContext;

        public int exceptionId { get; set; }
        public string exceptionMessage { get; set; }
        public string exceptionDetail { get; set; }

        // Constructor
        public VentureDbService(VenturesDbContext venturesDbContext){
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

        // ****************************************************** //
        // ************** SELECT METODLARI BAŞLANGICI *********** //
        // ****************************************************** //
        public async Task<List<object>> GetAllHeaders()
        {// Tüm ventureheader verilerini getiren metod
            var holder = await _venturesDbContext.venturesheader.ToListAsync(); // ventureheaders tablosundaki tüm veriyi alır

            if (holder == null || holder.Count == 0)
            {
                // ventureheaders tablosunda veri olup olmadığının kontrolünü yapar


                var exceptionHolder = await _venturesDbContext.exceptionlog
                                                              .OrderByDescending(vh => vh.Id)
                                                              .FirstOrDefaultAsync();
                // eğer boş gelirse oluşan null exception için exceptionlog tablosuna son insert edilen veriyi alır ve döndürür
                return new List<object> { exceptionHolder };

            }
            //ventureheaders tablosundaki tüm veriyi döndürür
            return holder.Cast<object>().ToList();
        }
        public async Task<List<object>> GetAllDetails()
        {// Tüm venturedetails verilerini getiren metod
            var holder = await _venturesDbContext.venturesdetails.ToListAsync(); // venturesdetails tablosundaki tüm veriyi alır
            if (holder == null || holder.Count == 0)
            {
                // venturesdetails tablosunda veri olup olmadığının kontrolünü yapar
                var exceptionHolder = await _venturesDbContext.exceptionlog
                                                              .OrderByDescending(vh => vh.Id)
                                                              .FirstOrDefaultAsync();
                // eğer boş gelirse oluşan null exception için exceptionlog tablosuna son insert edilen veriyi alır ve döndürür
                return new List<object> { exceptionHolder };
            }
            return holder.Cast<object>().ToList();
        }
        public async Task<List<object>> GetAllImg()
        {// Tüm venturesimg verilerini getiren metod
            var holder = await _venturesDbContext.venturesimg.ToListAsync(); // venturesimg tablosundaki tüm veriyi alır
            if (holder == null || holder.Count == 0)
            {
                // venturesimg tablosunda veri olup olmadığının kontrolünü yapar
                var exceptionHolder = await _venturesDbContext.exceptionlog
                                                              .OrderByDescending(vh => vh.Id)
                                                              .FirstOrDefaultAsync();
                // eğer boş gelirse oluşan null exception için exceptionlog tablosuna son insert edilen veriyi alır ve döndürür
                return new List<object> { exceptionHolder };
            }
            return holder.Cast<object>().ToList();
        }
        // ****************************************************** //
        // **************** SELECT METODLARI SONU *************** //
        // ****************************************************** //


        // ****************************************************** //
        // ************** INSERT METODLARI BAŞLANGICI *********** //
        // ****************************************************** //

        public object InsertNewVentureHeader(string header, int targetFund)
        {//VentureHeader tablosuna insert atan metod
            try
            {
                var newVentureHeader = new VenturesHeader()
                {
                    VentureHeader = header,
                    TargetFund = targetFund,
                    CreationDate = DateTime.Now,
                    HeaderThumbnail = "path/test"
                };//model oluşturuldu
                _venturesDbContext.venturesheader.Add(newVentureHeader);//insert işlemi
                _venturesDbContext.SaveChanges();//commit işlemi

                return newVentureHeader;
            }
            catch (DbUpdateException ex)//database commit işlemi yapılırken oluşan hata var mı kontrolü
            {
                var exceptionDbUpdate = new ExceptionLog()
                {
                    ExceptionId = 5,
                    Message = ex.Message,
                    Detail = "InsertNewVentureHeader"
                };
                return exceptionDbUpdate;
            }
            catch (Exception ex)// oluşan başka bir hata var mı kontrolü
            {
                var exceptionOthers = new ExceptionLog()
                {
                    ExceptionId = 5,
                    Message = ex.Message,
                    Detail = "InsertNewVentureHeader"
                };
                return exceptionOthers;
            }
        }
        public object InsertNewVentureDetail(int id, string description, int fundAmount, int minInvestValue, int numberOfInvestor)
        {//VentureDetail tablosuna insert atan metod
            try
            {
                var newVentureDetail = new VenturesDetail()
                {
                    VentureId = id,
                    Description = description,
                    FundAmount = fundAmount,
                    MinInvestValue = minInvestValue,
                    NumberOfInvestor = numberOfInvestor,
                }; // model oluşturuldu

                _venturesDbContext.venturesdetails.Add(newVentureDetail); //insert işlemi
                _venturesDbContext.SaveChanges(); // commit işlemi

                return newVentureDetail;
            }
            catch (DbUpdateException ex)//database commit işlemi yapılırken oluşan hata var mı kontrolü
            {
                var exceptionDbUpdate = new ExceptionLog()
                {
                    ExceptionId = 5,
                    Message = ex.Message,
                    Detail = "InsertNewVentureDetail"
                };
                return exceptionDbUpdate;
            }
            catch (Exception ex)// oluşan başka bir hata var mı kontrolü
            {
                var exceptionOthers = new ExceptionLog()
                {
                    ExceptionId = 5,
                    Message = ex.Message,
                    Detail = "InsertNewVentureDetail"
                };
                return exceptionOthers;
            }
        }
        public object InsertNewVentureImg(byte[] imgFile, string imgName, int orgId)
        {//venture tablosuna insert atmak için kullanılan metod
            try
            {
                var newVentureImg = new VenturesImg()
                {
                    ImgFile = imgFile,
                    ImgName = imgName,
                    OrgId = orgId,
                    CreationDate = DateTime.Now,
                }; // VentureImg modelini doldurur

                _venturesDbContext.venturesimg.Add(newVentureImg); //tabloya insert atar
                _venturesDbContext.SaveChanges(); // commit işlemi

                return newVentureImg;//object döndürür
            }
            catch (DbUpdateException ex) //database commit işlemi yapılırken oluşan hata var mı kontrolü
            {
                var exceptionDbUpdate = new ExceptionLog()
                {
                    ExceptionId = 5,
                    Message = ex.Message,
                    Detail = "InsertNewVentureImg"
                };
                return exceptionDbUpdate;
            }
            catch (Exception ex)// oluşan başka bir hata var mı kontrolü
            {
                var exceptionOthers = new ExceptionLog()
                {
                    ExceptionId = 5,
                    Message = ex.Message,
                    Detail = "InsertNewVentureImg"
                };
                return exceptionOthers;
            }
        }
        public void InsertWhenException(List<dynamic> excList)
        {// Constructor da exception yakaladığında exceptionlog tablosunda veri insert eden metod
            try
            {
                var newexception = new ExceptionLog()
                {
                    ExceptionId = excList[0],
                    Message = excList[1],
                    Detail = excList[2]
                }; //Exception log modelini oluşan hataya göre sınıf parametrelerini kullanarak doldurur


                _venturesDbContext.exceptionlog.Add(newexception); // tabloya veriyi ekler
                _venturesDbContext.SaveChanges(); // tablo değişikliğinin işlenmesi için commit atılır

                //return newexception;
            }
            catch (DbUpdateException ex) //database commit işlemi yapılırken oluşan hata var mı kontrolü
            {
                // Veritabanı güncelleme hatası
                var exceptionDbUpdate = new ExceptionLog()
                {
                    ExceptionId = 5,
                    Message = ex.Message,
                    Detail = "InsertWhenException"
                };

                //return exceptionDbUpdate;
            }
            catch (Exception ex) // oluşan başka bir hata var mı kontrolü
            {
                // Diğer hatalar
                var exceptionOthers = new ExceptionLog()
                {
                    ExceptionId = 6,
                    Message = ex.Message,
                    Detail = "InsertWhenException"
                };
                //return exceptionOthers;
            }
        }

        // ****************************************************** //
        // **************** INSERT METODLARI SONU *************** //
        // ****************************************************** //


    }
}
