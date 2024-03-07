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
        public async Task<List<UserAuth>> GetAllUserAuthenticationData(){
            return await _accountsDbContext.user_auth.ToListAsync();
        }

        //Kayıt olurken aynı email ile kaydedilmiş bir user var mı kontrolü yapan method
        public bool IsExistsInUserDb(string email){
            var control = _accountsDbContext.user_auth.FirstOrDefault(auth => auth.Email == email); // FirstOrDefault : varsa UserAuth döndürür yoksa null döndürür
            if(control == null){
                return false; // bu şekilde dönerse gelen email ile hesap oluşturulabilir
            }
            return true; // bu şekilde dönerse gelen email ile hesap oluşturulamaz
        }
        //

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
