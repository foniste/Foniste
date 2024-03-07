using FonApi.Database;
using FonApi.Models.Accounts;
using FonApi.Service;
using Microsoft.AspNetCore.Mvc;

namespace FonApi.Controllers
{
    // ****************************************************************** //
    // AccountController: Yalnızca Kullanıcı İşlemlerini Yapan Controller //
    // ****************************************************************** //

    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase {
        //Error Messages 
        private const string newUserIsNullException = "An error occurred while trying to register. Please try again.";
        private const string registeredUserException = "This user has already been registered.";
        private const string registrationSucceeded = "Registered";
        //
        private readonly AccountDbService _accountDbService;

        //Constructor
        public AccountController(AccountDbService accountDbService){
            _accountDbService = accountDbService ?? throw new ArgumentNullException(nameof(accountDbService));
        }
        //

        //* KAYIT EKRANI METODLARI BAŞLANGICI //

        // ! Kayıt olma ekranı için method başlangıcı //
        [HttpPost("/new/usr")]
        public async Task<IActionResult> CreateNewUser([FromBody] UserAuth newUser) {
            if(newUser == null) {
                return BadRequest(newUserIsNullException);
            }

            UserAuth template = new UserAuth{
                Email = newUser.Email,
                Password = newUser.Password,
                RoleId = 0,
                CreationDate = newUser.CreationDate
            };

            var result = await CheckUserByEmail(newUser.Email);
            switch (result){
                case "Available": //Insert atabilmek için şart sağlandı
                    _accountDbService.InsertNewUser(template); //yeni bir kullanıcı insert edildi
                    return Ok("Hesap Oluşturuldu");
                case "Unavailable": // Insert atabilmek için şart sağlanmadı, zaten kaydedilmiş bir hesap var 
                    return Ok("Hesap Oluşturulamaz");
                default:
                    return BadRequest("FonIste");
            }

        }
        // ! Kayıt olma ekranı için method sonu //


        // ! Email bazlı user varmı yok mu kontrolü //
        [HttpGet("isExists/{email?}")]
        public async Task<string> CheckUserByEmail([FromRoute] string email){
            
            try {
                bool control = _accountDbService.IsExistsInUserDb(email);
                if(!control == true) {
                    return "Available"; // hesap oluşturulabilir 
                }
                
                return "Unavailable"; // hesap oluşturulamaz
            }
            catch (Exception ex) {
                return "An error occured : \n" + ex.Message;
            }
        }
        // ! Email bazlı user varmı yok mu kontrol sonu //

        // * KAYIT EKRANI METODLARI SONU //
    }
}