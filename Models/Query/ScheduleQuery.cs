namespace ScheduleGym.Models.Query
{
    public class ScheduleQuery
    {
        public int? UserId { get; set; }
        public int? PlaceId { get; set; }
        public List<string>? Statuses { get; set; }
        public bool MyPlaces { get; set; } = false;
    }
}
