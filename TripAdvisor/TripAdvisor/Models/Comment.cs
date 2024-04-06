using System.Runtime.CompilerServices;

namespace TripAdvisor.Models
{
    public class Comment
    {
        public int id { get; set; }
        public string text { get; set; }
        public string state { get; set; }
        public int rating { get; set; }
        public string author { get; set; }
        public string deleted { get; set; }
        public int idTrip { get; set; }
        
    }
}
