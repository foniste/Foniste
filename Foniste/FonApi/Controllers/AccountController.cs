using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
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
        private object _userManager;

        //Constructor
        public AccountController(AccountDbService accountDbService, IConfiguration configuration)
        {
            _accountDbService = accountDbService ?? throw new ArgumentNullException(nameof(accountDbService));
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
                Email = newUser.Email,
                Password = newUser.Password,
                RoleId = 0,
                CreationDate = newUser.CreationDate
            };

            var result = await CheckUserByEmail(newUser.Email);
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
                bool control = _accountDbService.IsExistsInUserDb(email);
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
                currentUserAuth.Email,
                _accountDbService.HashSHA256(currentUserAuth.Password)
            );
            if (!control)
            {
                return Ok("Kullanıcı id si alınamadı");
            }
            else
            {
                var temp = _accountDbService.GetOrgIdByEmail(
                            currentUserAuth.Email,
                            _accountDbService.HashSHA256(currentUserAuth.Password)
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
        public async Task<IActionResult> EditOrganization(int organization_id,[FromBody] Organizations organization)
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
        //************************  deneme  ***  login  *****************

        public interface IUserService
        {
            Task<UserAuth> Authenticate(string username, string password);
        } 

        public class UserService : IUserService
        {
            private readonly List<UserAuth> _users = new List<UserAuth>
    {
        new UserAuth { UserId = 1, Email = "user1", Password = "hashedPassword1" },
        new UserAuth { UserId = 2, Email = "user2", Password = "hashedPassword2" }
    };

            public async Task<UserAuth> Authenticate(string username, string password)
            {
                var user = await Task.Run(() => _users.SingleOrDefault(u => u.Email == username && u.Password == HashPassword(password)));
                return user;
            }

            private string HashPassword(string password)
            {
                // Bu metodu gerçek bir hashleme algoritmasıyla değiştirmelisiniz (örneğin, bcrypt, PBKDF2, Argon2)
                return password; // Örnek amaçlı doğrudan şifreyi geri döndürüyoruz
            }
        }



        /*
        [ApiController]
        [Route("api/auth")]
        public class AuthController : ControllerBase
        {
            private readonly IUserService _userService;
            private readonly IJwtService _jwtService;

            public AuthController(IUserService userService, IJwtService jwtService)
            {
                _userService = userService;
                _jwtService = jwtService;
            }

            [HttpPost("login")]
            public async Task<IActionResult> Login(UserAuth model)
            {
                var user = await _userService.Authenticate(model.Email, model.Password);
                if (user == null)
                {
                    return Unauthorized("Invalid username or password");
                }

                var token = _jwtService.GenerateToken(user);
                return Ok(new { Token = token });
            }
        }

        //********************deneme sonu*******************************************

*/


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

       
    }
}