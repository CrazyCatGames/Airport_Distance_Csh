using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Airport_Distance.Calculators;
using Airport_Distance.Services;
using Newtonsoft.Json;

namespace Airport_Distance.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DistanceController : ControllerBase
    {
        private readonly IAirportService _airportService;
        private readonly IDistanceCalculator _distanceCalculator;

        public DistanceController(IAirportService airportService, IDistanceCalculator distanceCalculator)
        {
            _airportService = airportService;
            _distanceCalculator = distanceCalculator;
        }

        [HttpGet]
        [Route("between")]
        public async Task<IActionResult> GetDistance(string fromIata, string toIata)
        {
            try
            {
                var taskFromAirport = _airportService.GetAirportAsync(fromIata);
                var taskToAirport = _airportService.GetAirportAsync(toIata);

                await Task.WhenAll(taskFromAirport, taskToAirport);

                var fromAirport = taskFromAirport.Result;
                var toAirport = taskToAirport.Result;

                double distance = _distanceCalculator.CalculateDistance(fromAirport.Location.Latitude, fromAirport.Location.Longitude, toAirport.Location.Latitude, toAirport.Location.Longitude);
                return Ok(new {  distance, fromAirport, toAirport  });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}

