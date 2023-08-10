namespace CovidChart.API.Models
{
    public enum CityEnum
    {
        Istanbul = 1,
        Ankara = 2,
        Izmır = 3,
        Antalya = 4,
        Ordu = 5
    }

    public class Covid
    {
        public int Id { get; set; }
        public CityEnum City { get; set; }
        public int Count { get; set; }
        public DateTime CovidDate { get; set; }
    }
}
