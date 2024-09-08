namespace ScheduleGym.Models.Query
{
    public class PaginanationList<T>
    {
        public int Page { get; set; }
        public int Count { get; set; }
        public int PageSize { get; set; }
        public List<T> Items { get; set; }
    }
}
