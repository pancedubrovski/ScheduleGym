namespace ScheduleGym.Models.Responses
{
    public class AppoimnentResponse
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Description { get; set; }
        public Place Place { get; set; }

        public UserResponse? User { get; set; }
        public double TotalPrice { get; set; }

        
    }
}
