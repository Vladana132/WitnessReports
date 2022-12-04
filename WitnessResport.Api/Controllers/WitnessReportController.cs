using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WitnessReports.Common.DTO;
using WitnessReports.Interface.Services;

namespace WitnessReports.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WitnessReportController : ControllerBase
    {
        private readonly IWitnessReportService _witnessReportService;
        public WitnessReportController(IWitnessReportService witnessReportService)
        {
            _witnessReportService = witnessReportService;
        }

        [HttpPost("fugitive")]
        public async Task<IActionResult> ReportFugitiveAsync([FromBody] WitnessReportDto witnessReportDto)
        {
            await _witnessReportService.ReportFugitiveAsync(witnessReportDto);
            return Ok();
        }
    }
}
