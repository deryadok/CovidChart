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
            await _hubContext.Clients.All.SendAsync("RecieveCovidList", GetCovidChartList());
        }

        public List<CovidChartModel> GetCovidChartList()
        {
            List<CovidChartModel> covidCharts = new List<CovidChartModel>();

            using(var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT CovidDate, [1], [2], [3], [4], [5] FROM\r\n(SELECT [City], [Count], CAST([CovidDate] AS DATE) AS CovidDate FROM Covids) AS CovidT\r\nPIVOT\r\n(SUM([Count]) FOR City IN ([1], [2], [3], [4], [5])) AS PivotT\r\nORDER BY CovidDate";

                command.CommandType = CommandType.Text;

                _context.Database.OpenConnection();

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        CovidChartModel model = new CovidChartModel();

                        model.CovidDate = reader.GetDateTime(0).ToShortDateString();

                        Enumerable.Range(1, 5).ToList().ForEach(x =>
                        {
                            if (DBNull.Value.Equals(reader[x]))
                            {
                                model.Counts.Add(0);
                            }
                            else
                            {
                                model.Counts.Add(reader.GetInt32(x));
                            }
                        });

                        covidCharts.Add(model);
                    }
                }
            }

            _context.Database.CloseConnection();

            return covidCharts;
        }
    }
}
