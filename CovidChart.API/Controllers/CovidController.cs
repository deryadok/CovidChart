using CovidChart.API.Models;
using CovidChart.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace CovidChart.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CovidController : ControllerBase
    {
        private readonly CovidService _service;

        public CovidController(CovidService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> SaveCovid(Covid covid)
        {
            await _service.SaveCovid(covid);
            List<Covid> covidList = _service.GetList().ToList();
            return Ok(_service.GetCovidChartList());
        }

        [HttpGet]
        public IActionResult InitializeCovid()
        {
            for (int i = 1; i <= 10; i++)
            {
                Random rnd = new Random();
                var newCovid = new Covid()
                {
                    City1 = rnd.Next(100, 1000),
                    City2 = rnd.Next(100, 1000),
                    City3 = rnd.Next(100, 1000),
                    City4 = rnd.Next(100, 1000),
                    City5 = rnd.Next(100, 1000),

                    CovidDate = DateTime.Now.AddDays(i),
                };

                _service.SaveCovid(newCovid).Wait();

                Thread.Sleep(1000);

            }

            return Ok("Covid19 dataları veritabanına kaydedildi.");
        }
    }
}
