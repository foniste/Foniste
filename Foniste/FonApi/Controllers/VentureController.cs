using FonApi.Models.Ventures;
using FonApi.Service;
using Microsoft.AspNetCore.Mvc;

namespace FonApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VentureController : ControllerBase
    {
        private readonly VentureDbService _ventureDbService;

        public VentureController(VentureDbService ventureDbService) {
            _ventureDbService = ventureDbService ?? throw new ArgumentNullException(nameof(ventureDbService));
        }

        [HttpGet("/myprofile/ventures/{organizationId}")]
        public async Task<IActionResult> GetVenturesByOrganizationId(int organizationId)
        {
            var ventures = await _ventureDbService.GetVenturesByOrganizationIdAsync(organizationId);
            if (ventures == null || ventures.Count == 0)
            {
                return NotFound();
            }

            return Ok(ventures);
        }


        [HttpPost("/new/ventures")]
        public async Task<IActionResult> InsertNewVenturesByOrgId([FromBody] VenturesAll venturesAll)
        {
            try
            {
                if (venturesAll == null)
                {
                    return NotFound("null_ventures");
                }

                VenturesAll template = new VenturesAll
                { 
                    venture_header = venturesAll.venture_header,
                    venture_description = venturesAll.venture_description,
                    organization_id = venturesAll.organization_id,
                    num_of_investor = venturesAll.num_of_investor,
                    min_invest_value = venturesAll.min_invest_value,
                    header_thumbnail =  venturesAll.header_thumbnail,
                    fund_amount = venturesAll.fund_amount,
                    target_fund = venturesAll.target_fund
                };

                _ventureDbService.InsertNewVenture(template);
                _ventureDbService.Initialize();

                return Ok(venturesAll);
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
            
        }
    }
}
