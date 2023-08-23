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
        public int City1 { get; set; }
        public int City2 { get; set; }
        public int City3 { get; set; }
        public int City4 { get; set; }
        public int City5 { get; set; }
        public DateTime CovidDate { get; set; }
    }
}
