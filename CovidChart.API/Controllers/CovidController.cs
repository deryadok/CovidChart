using CovidChart.API.Models;
using CovidChart.API.Services;
using Microsoft.AspNetCore.Http;
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
            Random rnd = new Random();
            Enumerable.Range(1, 10).ToList().ForEach(x =>
            {
                foreach (CityEnum item in Enum.GetValues(typeof(CityEnum)))
                {
                    var newCovid = new Covid()
                    {
                        City = item,
                        Count = rnd.Next(100, 1000),
                        CovidDate = DateTime.Now.AddDays(x),
                    };

                    _service.SaveCovid(newCovid).Wait();

                    Thread.Sleep(1000);
                }
            });

            return Ok("Covid19 dataları veritabanına kaydedildi.");
        }
    }
}
