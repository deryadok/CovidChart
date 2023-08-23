using CovidChart.API.Models;
using CovidChart.API.Services;
using Microsoft.AspNetCore.SignalR;

namespace CovidChart.API.Hubs
{
    public class CovidHub : Hub
    {
        private readonly CovidService _covidService;

        public CovidHub(CovidService covidService)
        {
            _covidService = covidService;
        }

        public async Task GetCovidList()
        {
            await Clients.All.SendAsync("RecieveList", _covidService.GetCovidChartList());
        }

        public async Task GetCityCovidList(string city)
        {
            byte selectedCity = 0;

            switch (city)
            {
                case "istanbul":
                    selectedCity = 1;
                    break;
                case "ankara":
                    selectedCity = 2;
                    break;
                case "izmir":
                    selectedCity = 3;
                    break;
                case "antalya":
                    selectedCity = 4;
                    break;
                case "ordu":
                    selectedCity = 5;
                    break;
            }

            await Clients.Group(city).SendAsync("RecieveList", _covidService.GetCovidChartListByCity(selectedCity));

        }

        public async Task AddCityGroup(string city)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, city);
        }

        public async Task RemoveFromCityGroup(string city)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, city);
        }
    }
}
