using Foniste.Models.Ventures;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net.Http.Headers;

namespace Foniste.Controllers
{
    public class ProjectController : Controller
    {
        public async Task<IActionResult> Index()
        {
            // Modeli doldurun, örneğin bir API'den veri alın
            List<VenturesAll> ventures = await GetVenturesFromApi();
            return View(ventures);
        }

        public async Task<IActionResult> ProjectDetails(int id)
        {
            VenturesAll project = await GetProjectFromApi(id);
            if (project == null)
            {
                return NotFound();
            }
            return View(project);
        }
        public async Task<List<VenturesAll>> GetVenturesFromApi()
        {
            List<VenturesAll> ventures = new List<VenturesAll>();

            try
            {
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync("https://localhost:7173/all/ventures"))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        ventures = JsonConvert.DeserializeObject<List<VenturesAll>>(apiResponse);
                    }
                }
            }
            catch (Exception ex)
            {
                // Hata durumunda gerekli işlemleri yapabilirsiniz
                Console.WriteLine("API'den veri alınırken hata oluştu: " + ex.Message);
            }

            return ventures;
        }
        public async Task<VenturesAll> GetProjectFromApi(int id)
        {
            VenturesAll project = null;

            try
            {
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync($"https://localhost:7173/venture/{id}"))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        project = JsonConvert.DeserializeObject<VenturesAll>(apiResponse);
                    }
                }
            }
            catch (Exception ex)
            {
                // Hata durumunda gerekli işlemleri yapabilirsiniz
                Console.WriteLine("API'den veri alınırken hata oluştu: " + ex.Message);
            }

            return project;
        }
    }
}
