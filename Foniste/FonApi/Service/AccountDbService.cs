using FonApi.Database;
using FonApi.Interfaces;
using FonApi.Models.Accounts;
using Microsoft.EntityFrameworkCore;

namespace FonApi.Service
{
    public class AccountDbService : IAccountDbService
    {
        private readonly AccountsDbContext _accountsDbContext;

        //Constructor
        public AccountDbService(AccountsDbContext accountsDbContext)
        {
            _accountsDbContext = accountsDbContext ?? throw new ArgumentNullException(nameof(accountsDbContext));
            //_accountsDbContext boş gelmesi durumunda exception
        }
        //

        // ?---------------------------------------------------------------------------------//
        // ?-----------------------------------USER DATA-------------------------------------//
        // ?---------------------------------------------------------------------------------//

        // Tüm Kullanıcıları getiren method
        public async Task<List<Users>> GetAllUsers()
        {
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
        public int GetOrgIdByEmail(string email, string password)
        {
            try
            {
                int holder = _accountsDbContext.user_auth
                .Where(user => user.Email == email)
                .Select(user => user.UserId).FirstOrDefault();

                if (holder == null)
                {
                    return 0;
                }

                return holder;
            }
            catch (System.Exception)
            {
                throw;
            }
        }
        //

        //Kayıt olurken aynı email ile kaydedilmiş bir user var mı kontrolü yapan method
        public bool IsExistsInUserDb(string email)
        {
            var control = _accountsDbContext.user_auth.FirstOrDefault(auth => auth.Email == email); // FirstOrDefault : varsa UserAuth döndürür yoksa null döndürür
            if (control == null)
            {
                return false; // bu şekilde dönerse gelen email ile hesap oluşturulabilir
            }
            return true; // bu şekilde dönerse gelen email ile hesap oluşturulamaz
        }
        //

        //Kayıt olma işlemini tamamlamak için tabloya insert atma metodu
        public void InsertNewUser(UserAuth userAuth)
        {
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
        public bool Login(string email, string password)
        {
            var temp = _accountsDbContext
                        .user_auth
                        .Where(c => c.Email == email && c.Password == password)
                        .ToList();

            if (temp.Count == 0)
            {
                return false;
            }
            return true;
        }
        //

        // ?---------------------------------------------------------------------------------//
        // ?---------------------------------------------------------------------------------//
        // ?---------------------------------------------------------------------------------//





        // ?---------------------------------------------------------------------------------//
        // ?------------------------------------LOGGING--------------------------------------//
        // ?---------------------------------------------------------------------------------//
        // Tüm login işlemlerinin tutulduğu log verilerini getiren method
        public async Task<List<LoginLog>> GetAllLog()
        {
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
        public async Task<List<Organizations>> GetAllOrganization()
        {
            return await _accountsDbContext.organization.ToListAsync();
        }

        public void InsertNewOrganization(Organizations org)
        {
            try
            {
                _accountsDbContext.organization.Add(org);
                _accountsDbContext.SaveChanges();
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public void AddOrUpdateOrganizationAsync(Organizations organization)
        {
            var existingOrganization = _accountsDbContext.organization
                                                     .FirstOrDefault(o => o.OrgId == organization.OrgId);

            if (existingOrganization == null)
            {
                // Kayıt yoksa yeni kayıt ekle
                _accountsDbContext.organization.Add(organization);
            }
            else
            {
                // Yalnızca değişiklik olan alanları güncelle
                if (existingOrganization.PhoneNumber1 != organization.PhoneNumber1)
                    existingOrganization.PhoneNumber1 = organization.PhoneNumber1;

                if (existingOrganization.PhoneNumber2 != organization.PhoneNumber2)
                    existingOrganization.PhoneNumber2 = organization.PhoneNumber2;

                if (existingOrganization.PhoneNumber3 != organization.PhoneNumber3)
                    existingOrganization.PhoneNumber3 = organization.PhoneNumber3;

                if (existingOrganization.Address != organization.Address)
                    existingOrganization.Address = organization.Address;

                if (existingOrganization.IBAN != organization.IBAN)
                    existingOrganization.IBAN = organization.IBAN;

                if (existingOrganization.OrganizationName != organization.OrganizationName)
                    existingOrganization.OrganizationName = organization.OrganizationName;

                // Değişiklikleri kaydet
                _accountsDbContext.organization.Update(existingOrganization);
            }

            _accountsDbContext.SaveChanges();
        }

        //
        // ?---------------------------------------------------------------------------------//
        // ?---------------------------------------------------------------------------------//
        // ?---------------------------------------------------------------------------------//





        // ?---------------------------------------------------------------------------------//
        // ?-------------------------------------ROLE----------------------------------------//
        // ?---------------------------------------------------------------------------------//
        // Tüm role bilgilerini getiren method
        public async Task<List<Role>> GetAllRole()
        {
            return await _accountsDbContext.role.ToListAsync();
        }
        //
        // ?---------------------------------------------------------------------------------//
        // ?---------------------------------------------------------------------------------//
        // ?---------------------------------------------------------------------------------//


        // Veritabanındaki DML sorgularının değişikliklerinin sağlanması için api de yazdığın fonskiyona bunu ekle try bloğunda çalıştır.
        public void Initialize()
        {
            _accountsDbContext.SaveChanges();
        }
        //

    }
}
