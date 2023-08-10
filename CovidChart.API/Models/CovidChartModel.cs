namespace CovidChart.API.Models
{
    public class CovidChartModel
    {
        public string CovidDate { get; set; }
        public List<int> Counts { get; set; } = new List<int>();
    }
}
