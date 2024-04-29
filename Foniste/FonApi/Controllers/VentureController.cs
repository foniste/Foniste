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
        

        
        [HttpGet("/ventures")]
        public async Task<IActionResult> GetAllVentures()
        {// Tüm girişimleri getiren metod
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

        [HttpGet("ventures/all/details")]
        public async Task<IActionResult> GetAllVentureDetails()
        {// Tüm girişimleri getiren metod
            try
            {
                var ventures = await _ventureDbService.GetAllDetails();
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

        [HttpGet("ventures/all/img")]
        public async Task<IActionResult> GetAllVentureImg()
        {// Tüm girişimleri getiren metod
            try
            {
                var ventures = await _ventureDbService.GetAllImg();
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
    }
}
