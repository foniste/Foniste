using Foniste.Models.Accounts;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace Foniste.Controllers
{
	public class SigninController : Controller
	{
		// GET: SigninController
		public ActionResult Index()
		{
			return View();
		}

        public IActionResult Privacy()
        {
            return View();
        }

        public async Task<IActionResult> Signin(UserAuth newUser)
		{
			try
			{
				using (var client = new HttpClient())
				{
					client.BaseAddress = new Uri("https://localhost:7173"); // API'nin adresini belirtin
					client.DefaultRequestHeaders.Accept.Clear();
					client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

					// Yeni kullanıcıyı API'ye gönder
					HttpResponseMessage response = await client.PostAsJsonAsync("/new/usr", newUser);
					if (response.IsSuccessStatusCode)
					{
						string result = await response.Content.ReadAsStringAsync();
						if(result == "Hesap Oluşturuldu")
						{
							return BadRequest();
						}

                        string jwtToken = await response.Content.ReadAsStringAsync(); // Burada JWT token'ını nasıl alacağınıza bağlı olarak değişebilir
                        HttpContext.Session.SetString("JWTToken", jwtToken);
                        return RedirectToAction("Index", "Home");
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
