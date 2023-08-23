using CovidChart.API.Hubs;
using CovidChart.API.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace CovidChart.API.Services
{
    public class CovidService
    {
        private readonly AppDbContext _context;

        private readonly IHubContext<CovidHub> _hubContext;

        public CovidService(AppDbContext context, IHubContext<CovidHub> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
        }

        public IQueryable<Covid> GetList()
        {
            return _context.Covids.AsQueryable();
        }

        public async Task SaveCovid(Covid covid)
        {
            await _context.Covids.AddAsync(covid);
            await _context.SaveChangesAsync();
            //await _hubContext.Clients.All.SendAsync("RecieveList", GetCovidChartList());
            await _hubContext.Clients.Groups("all").SendAsync("RecieveList", GetCovidChartList());
            await _hubContext.Clients.Groups("istanbul").SendAsync("RecieveList", GetCovidChartListByCity(1));
            await _hubContext.Clients.Groups("ankara").SendAsync("RecieveList", GetCovidChartListByCity(2));
            await _hubContext.Clients.Groups("izmir").SendAsync("RecieveList", GetCovidChartListByCity(3));
            await _hubContext.Clients.Groups("antalya").SendAsync("RecieveList", GetCovidChartListByCity(4));
            await _hubContext.Clients.Groups("ordu").SendAsync("RecieveList", GetCovidChartListByCity(5));
        }

        public List<CovidChartModel> GetCovidChartList()
        {
            var covidData = _context.Covids.ToList();
            List<CovidChartModel> chartModel = new List<CovidChartModel>();

            foreach (var data in covidData)
            {
                var modifiedData = new CovidChartModel
                {
                    CovidDate = data.CovidDate.ToShortDateString(),
                    City1 = data.City1,
                    City2 = data.City2,
                    City3 = data.City3,
                    City4 = data.City4,
                    City5 = data.City5
                };

                chartModel.Add(modifiedData);
            }

            return chartModel;
        }

        public List<CovidChartModel> GetCovidChartListByCity(byte city)
        {
            List<Covid> covidData = _context.Covids.ToList();
            List<CovidChartModel> chartModel = new List<CovidChartModel>();

            foreach (var data in covidData)
            {
                var modifiedData = new CovidChartModel
                {
                    CovidDate = data.CovidDate.ToShortDateString(),
                    City1 = city == 1 ? data.City1 : 0,
                    City2 = city == 2 ? data.City2 : 0,
                    City3 = city == 3 ? data.City3 : 0,
                    City4 = city == 4 ? data.City4 : 0,
                    City5 = city == 5 ? data.City5 : 0
                };

                chartModel.Add(modifiedData);
            }

            return chartModel;
        }
    }
}
