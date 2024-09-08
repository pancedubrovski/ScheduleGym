using ScheduleGym.Models.Enums;

namespace ScheduleGym.Models.Responses
{
    public class PlaceResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Capacity { get; set; }
        public String Kind { get; set; }
        public string City { get; set; }
        public string Multiplicity { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public double Rating { get; set; }
        public List<AvalableTerms> avalableTerms { get; } = new List<AvalableTerms>();
        public List<Appointments> appointments { get; } = new List<Appointments>();
        public List<Review> reviewes { get; } = new List<Review>();
    }
}
