using Foniste.Models.Accounts;
using Microsoft.AspNetCore.Http;
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
						return Ok(result);
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
