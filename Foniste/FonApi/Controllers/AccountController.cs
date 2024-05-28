using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FonApi.Interfaces;
using FonApi.Models.Accounts;
using FonApi.Service;
using Microsoft.AspNetCore.Identity;
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

        //Error Messages 
        private const string newUserIsNullException = "An error occurred while trying to register. Please try again.";
        //
        private readonly AccountDbService _accountDbService;
		private object _userManager;

		//Constructor
		public AccountController(AccountDbService accountDbService, IConfiguration configuration){
            _accountDbService = accountDbService ?? throw new ArgumentNullException(nameof(accountDbService));
            _configuration = configuration;
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
                    return BadRequest("Hata!");
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
        //! Email bazlı user varmı yok mu kontrol sonu 

        //* KAYIT EKRANI METODLARI SONU 


        //* GİRİŞ EKRANI METODLARI BAŞLANGICI 
        //! Email ve Şifre Kullanarak Login Methodu Başlangıcı 



        [HttpPost("/current/user")]
        public async Task<IActionResult> LoginByEmailPassword([FromBody] UserAuth currentUserAuth)
        {
            // Kullanıcının veritabanında doğrulama kontrolü yapılır
            var control = _accountDbService.Login(
                currentUserAuth.Email, currentUserAuth.Password
            //_accountDbService.HashSHA256(currentUserAuth.Password)
            );

            // Eğer kullanıcı doğrulanamazsa
            if (!control)
            {
                // Başarısız durumda "Kullanıcı id si alınamadı" mesajı ile birlikte OK (200) döndürülür
                return Ok("Kullanıcı id si alınamadı");
            }
            else
            {
                // Kullanıcı doğrulanırsa, kullanıcının kimliği alınır
                var temp = _accountDbService.GetUserIdByEmail(
                    currentUserAuth.Email, 
                    currentUserAuth.Password
                    //_accountDbService.HashSHA256(currentUserAuth.Password)
                );

                // Eğer kullanıcı kimliği alınamazsa
                if (temp == 0)
                {
                    // Başarısız durumda "Id alınırken bir sorun oluştu." mesajı ile birlikte OK (200) döndürülür
                    return Ok("Id alınırken bir sorun oluştu.");
                }
                else
                {
                    // Kullanıcı kimliği başarılı şekilde alınırsa, başarılı giriş mesajı ve JWT token ile birlikte OK (200) döndürülür
                    //return Ok("Giriş Başarılı \n" + "Kullanıcı id si :" + temp/* + GenerateJwtToken(currentUserAuth.Email)*/);
                    // Giriş başarılı ise kullanıcı adını yanıta ekle
                    var user = _accountDbService.GetUserByEmail(currentUserAuth.Email);
                    return Ok(new { UserId = temp, Username = user.Email });
                }
            }
        }


        private string GenerateJwtToken(string email)
        {
            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Secret"]);
            var issuer = _configuration["Jwt:Issuer"];
            var audience = _configuration["Jwt:Audience"];
            var expiry = DateTime.Now.AddMinutes(Convert.ToDouble(_configuration["Jwt:ExpirationInMinutes"]));

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer,
                audience,
                claims,
                expires: expiry,
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        // ! Email ve Şifre Kullanarak Login Methodu Son //

        // * GİRİŞ EKRANI METODLARI SONU //
    }
}