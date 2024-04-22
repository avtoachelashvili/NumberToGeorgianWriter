using Microsoft.AspNetCore.Mvc;
using NumberToGeorgianWriter.Core.Contracts;

namespace NumberToGeorgianWriter.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly INumberToGeoContract _numberToGeoService;
        public HomeController(INumberToGeoContract numberToGeoService)
        {
            _numberToGeoService = numberToGeoService;
        }

        [HttpPost]
        [Route("parse-number")]
        public async Task<IActionResult> ParseNumber([FromBody] string number)
        {
            var result = await _numberToGeoService.ConvertNumberAsync(number);
            return Ok(result);
        }
    }
}
