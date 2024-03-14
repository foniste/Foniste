using FonApi.Service;
using Microsoft.AspNetCore.Mvc;

namespace FonApi.Controllers
{
    // ****************************************************************** //
    // VentureController: Girişimlerin olduğu verileri getiren controller //
    // ****************************************************************** //


    [Route("[controller]")]
    [ApiController]
    public class VentureController : ControllerBase
    {
        private readonly VentureDbService _ventureDbService;

        //Constructor
        public VentureController(VentureDbService ventureDbService)
        {
            _ventureDbService = ventureDbService;
        }
        //

        // Tüm girişimleri getiren metod
        [HttpGet("allventures")]
        public async Task<IActionResult> GetAllVentures() 
        {
            try
            {
                var ventures = await _ventureDbService.GetAllHeaders();

                if (ventures != null && ventures.Count > 0)
                {
                    return Ok(ventures);
                }

                return Ok(ventures);
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }
        //
    }
}
