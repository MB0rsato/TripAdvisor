namespace TripAdvisor.Models
{
    public class Trip
    {
        public int id { get; set; }
        public DateTime date { get; set; }
        public string location { get; set; }
        public string classes { get; set; }
        public int duration { get; set; }
        public string type { get; set; }
        public int price { get; set; }
        public string picture { get; set; }
        public string description { get; set; }
        public int averageRating { get; set; }

    }
}
