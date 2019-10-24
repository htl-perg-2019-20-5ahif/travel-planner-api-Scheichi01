using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TravelplannerLibrary;

namespace TravelplannerAPI.Controllers
{
    [ApiController]
    [Route("api/travelPlan")]
    public class TravelplannerController : ControllerBase
    {
        private static HttpClient HttpClient
             = new HttpClient() { BaseAddress = new Uri("https://cddataexchange.blob.core.windows.net/data-exchange/htl-homework/") };

        [HttpGet]
        public async Task<IActionResult> GetTravelPlan([FromQuery] string from, [FromQuery] string to, [FromQuery] string start)
        {
            var schedule = GetSchedule().Result;
            Travelplanner tp = new Travelplanner(schedule);
            var route = tp.FindConnection(from, to, start);
            if (route != null)
            {
                return Ok(route);
            }
            else
            {
                return NotFound();
            }
        }

        public async Task<Schedule[]> GetSchedule()
        {
            var response = await HttpClient.GetAsync("travelPlan.json");
            response.EnsureSuccessStatusCode();
            var result = JsonSerializer.Deserialize<Schedule[]>(await response.Content.ReadAsStringAsync());
            return result;
        }
    }
}
