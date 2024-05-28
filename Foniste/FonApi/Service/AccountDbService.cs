using System.Security.Cryptography;
using System.Text;
using FonApi.Database;
using FonApi.Interfaces;
using FonApi.Models.Accounts;
using Microsoft.EntityFrameworkCore;

namespace FonApi.Service {
    public class AccountDbService : IAccountDbService {
        private readonly AccountsDbContext _accountsDbContext;

        //Constructor
        public AccountDbService(AccountsDbContext accountsDbContext) {
            _accountsDbContext = accountsDbContext ?? throw new ArgumentNullException(nameof(accountsDbContext));
            //_accountsDbContext boş gelmesi durumunda exception
        }
        //

        // ?---------------------------------------------------------------------------------//
        // ?-----------------------------------USER DATA-------------------------------------//
        // ?---------------------------------------------------------------------------------//
        // Tüm Kullanıcıları getiren method
        public async Task<List<Users>> GetAllUsers(){
            return await _accountsDbContext.users.ToListAsync();
        }
        //
        // ?---------------------------------------------------------------------------------//
        // ?---------------------------------------------------------------------------------//
        // ?---------------------------------------------------------------------------------//
        



        // ?---------------------------------------------------------------------------------//
        // ?---------------------------------AUTHENTICATION----------------------------------//
        // ?---------------------------------------------------------------------------------//

        // Tüm kullancıların auth datasını getiren method
        public int GetUserIdByEmail(string email,string password){
            try {
                int holder = _accountsDbContext.user_auth
                .Where(user => user.Email == email)
                .Select(user => user.UserId).FirstOrDefault();

                if(holder == null){
                    return 0;
                }
                
                return holder;
            }
            catch (System.Exception) {
                throw;
            }
        }

        public UserAuth GetUserByEmail(string email)
        {
            // Veritabanından kullanıcıyı e-posta adresine göre al
            return _accountsDbContext.user_auth.FirstOrDefault(u => u.Email == email);
        }




        //

        //Kayıt olurken aynı email ile kaydedilmiş bir user var mı kontrolü yapan method
        public bool IsExistsInUserDb(string email){
            var control = _accountsDbContext.user_auth.FirstOrDefault(auth => auth.Email == email); // FirstOrDefault : varsa UserAuth döndürür yoksa null döndürür
            if(control == null){
                return false; // bu şekilde dönerse gelen email ile hesap oluşturulabilir
            }
            return true; // bu şekilde dönerse gelen email ile hesap oluşturulamaz
        }
        //

        //Kayıt olma işlemini tamamlamak için tabloya insert atma metodu
        public void InsertNewUser(UserAuth userAuth){
            try
            {
                _accountsDbContext.user_auth.Add(userAuth);
                _accountsDbContext.SaveChanges();
            }
            catch (System.Exception)
            {
                throw;
            }
        }
        //

        // Login methodu
        public bool Login(string email,string password){
            var temp = _accountsDbContext
                        .user_auth
                        .Where(c => c.Email == email && c.Password == password)
                        .ToList();

            if(temp.Count == 0) {
                return false;
            }
            return true;
        }
        

       // Sha-256 şifreleme metodu
        public string HashSHA256(string input)
        {
            SHA256 sha256 = SHA256.Create();

            byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            byte[] hashBytes = sha256.ComputeHash(inputBytes);
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < hashBytes.Length; i++)
            {
                stringBuilder.Append(hashBytes[i].ToString("x2"));
            }
            return stringBuilder.ToString();
        }


        // ?---------------------------------------------------------------------------------//
        // ?---------------------------------------------------------------------------------//
        // ?---------------------------------------------------------------------------------//





        // ?---------------------------------------------------------------------------------//
        // ?------------------------------------LOGGING--------------------------------------//
        // ?---------------------------------------------------------------------------------//
        // Tüm login işlemlerinin tutulduğu log verilerini getiren method
        public async Task<List<LoginLog>> GetAllLog(){
            return await _accountsDbContext.login_log.ToListAsync();
        }
        //
        // ?---------------------------------------------------------------------------------//
        // ?---------------------------------------------------------------------------------//
        // ?---------------------------------------------------------------------------------//



        // ?---------------------------------------------------------------------------------//
        // ?-----------------------------------ORGANIZATION----------------------------------//
        // ?---------------------------------------------------------------------------------//
        // Tüm organizations bilgilerini getiren method
        public async Task<List<Organizations>> GetAllOrganization(){
            return await _accountsDbContext.organization.ToListAsync();
        }
        //
        // ?---------------------------------------------------------------------------------//
        // ?---------------------------------------------------------------------------------//
        // ?---------------------------------------------------------------------------------//





        // ?---------------------------------------------------------------------------------//
        // ?-------------------------------------ROLE----------------------------------------//
        // ?---------------------------------------------------------------------------------//
        // Tüm role bilgilerini getiren method
        public async Task<List<Role>> GetAllRole(){
            return await _accountsDbContext.role.ToListAsync();
        }
        //
        // ?---------------------------------------------------------------------------------//
        // ?---------------------------------------------------------------------------------//
        // ?---------------------------------------------------------------------------------//
        
    }
}
