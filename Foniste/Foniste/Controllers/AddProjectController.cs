using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using Foniste.Models.Ventures;

namespace Foniste.Controllers
{
    public class AddProjectController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        
        public async Task<IActionResult> AddProject(VenturesAll newVenture)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:7173"); // API'nin adresi
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    // Yeni projeyi API'ye gönderilir
                    HttpResponseMessage response = await client.PostAsJsonAsync("/new/ventures", newVenture);
                    if (response.IsSuccessStatusCode)
                    {
                        string result = await response.Content.ReadAsStringAsync();
                        return RedirectToAction("Index","Project");
                    }
                    else
                    {
                        // API'den gelen hata mesajını alır ve gösterir
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
