using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using FonApi.Models.Accounts;
using FonApi.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;


namespace FonApi.Controllers
{
    // ****************************************************************** //
    // AccountController: Yalnızca Kullanıcı İşlemlerini Yapan Controller //
    // ****************************************************************** //

    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        //
        private readonly AccountDbService _accountDbService;
        private readonly EncryptionService _encryptionService;

        //Constructor
        public AccountController(AccountDbService accountDbService, IConfiguration configuration, EncryptionService encryptionService)
        {
            _accountDbService = accountDbService ?? throw new ArgumentNullException(nameof(accountDbService));
            _encryptionService = encryptionService ?? throw new ArgumentNullException(nameof(encryptionService));
            _configuration = configuration;
        }
        //

        //* KAYIT EKRANI METODLARI BAŞLANGICI

        // ! Kayıt olma ekranı için method başlangıcı //
        [HttpPost("/new/usr")]
        public async Task<IActionResult> CreateNewUser([FromBody] UserAuth newUser)
        {
            if (newUser == null)
            {
                return BadRequest();
            }

            UserAuth template = new UserAuth
            {
                Email = _encryptionService.Encrypt(newUser.Email),
                Password = _encryptionService.Encrypt(newUser.Password),
                RoleId = 0,
                OrgId = 0
            };

            var result = await CheckUserByEmail(_encryptionService.Encrypt(newUser.Email));
            switch (result)
            {
                case "Available": //Insert atabilmek için şart sağlandı
                    _accountDbService.InsertNewUser(template); //yeni bir kullanıcı insert edildi
                    return Ok("Hesap Oluşturuldu");
                case "Unavailable": // Insert atabilmek için şart sağlanmadı, zaten kaydedilmiş bir hesap var 
                    return Ok("Hesap Oluşturulamaz");
                default:
                    return BadRequest("Hata!");
            }

        }
        // ! Kayıt olma ekranı için method sonu //

        // ! Email bazlı user varmı yok mu kontrolü //
        [HttpGet("isExists/{email?}")]
        public async Task<string> CheckUserByEmail([FromRoute] string email)
        {
            try
            {
                bool control = _accountDbService.IsExistsInUserDb(_encryptionService.Encrypt(email));
                if (!control == true)
                {
                    return "Available"; // hesap oluşturulabilir 
                }

                return "Unavailable"; // hesap oluşturulamaz
            }
            catch (Exception ex)
            {
                return "An error occured : \n" + ex.Message;
            }
        }
        //! Email bazlı user varmı yok mu kontrol sonu 

        //* KAYIT EKRANI METODLARI SONU 


        //* GİRİŞ EKRANI METODLARI BAŞLANGICI   

        //! Email ve Şifre Kullanarak Login Methodu Başlangıcı 

        [HttpPost("current/user")]
        public async Task<IActionResult> LoginByEmailPassword([FromBody] UserAuth currentUserAuth)
        {
            var control = _accountDbService.Login( //Bu methodda email ve şifre parametreleri bazlı kontrol yapılıyor 
               _encryptionService.Encrypt(currentUserAuth.Email),
               _encryptionService.Encrypt(currentUserAuth.Password)
            );
            if (!control)
            {
                return Ok("Kullanıcı id si alınamadı");
            }
            else
            {
                var temp = _accountDbService.GetOrgIdByEmail(
                            _encryptionService.Encrypt(currentUserAuth.Email),
                            _encryptionService.Encrypt(currentUserAuth.Password)
                            );

                if (temp == 0)
                {
                    return Ok("Id alınırken bir sorun oluştu.");
                }
                return Ok(temp);
            }
        }
        // * GİRİŞ EKRANI METODLARI SONU //

        [HttpPost("/profile/organization/{organization_id?}")]
        public async Task<IActionResult> EditOrganization(int organization_id, [FromBody] Organizations organization)
        {
            Organizations tmp = new Organizations
            {
                OrgId = organization_id,
                OrganizationName = organization.OrganizationName,
                Address = organization.Address,
                IBAN = organization.IBAN,
                PhoneNumber1 = organization.PhoneNumber1,
                PhoneNumber2 = organization.PhoneNumber2,
                PhoneNumber3 = organization.PhoneNumber3
            };

            _accountDbService.AddOrUpdateOrganizationAsync(tmp);
            return Ok();

        }

    }
}