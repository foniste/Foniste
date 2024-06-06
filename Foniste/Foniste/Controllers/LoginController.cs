using Foniste.Models.Accounts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace Foniste.Controllers
{
    public class LoginController : Controller
    {
        // GET: LoginController
        public ActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Login(UserAuth userCredentials)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:7173"); // API adresini belirtin
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    // Kullanıcı kimlik bilgilerini API'ye gönder

                    

                    HttpResponseMessage response = await client.PostAsJsonAsync("/current/user", userCredentials);
                    if (response.IsSuccessStatusCode)
                    {
                        // Giriş başarılıysa, API'den gelen yanıtı oku
                        string result = await response.Content.ReadAsStringAsync();

                        // Token veya diğer giriş bilgilerini işleyin ve saklayın (örneğin Cookie'de saklayabilirsiniz)

                        // Başarılı giriş sonrası yönlendirme yapabilirsiniz
                        return RedirectToAction("Index", "Home"); // Örnek olarak HomeController'ın Index metodu
                    }
                    else
                    {
                        // API'den gelen hata mesajını al ve göster
                        string error = await response.Content.ReadAsStringAsync();
                        return BadRequest(error);
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Bir hata oluştu: {ex.Message}");
            }
        }

    }
}
