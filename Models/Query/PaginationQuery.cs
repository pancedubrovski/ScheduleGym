namespace ScheduleGym.Models.Query
{
    public class PaginationQuery
    {
        public int PageSize { get; set; } = 10;
        public int Page { get; set; } = 1;
    }
}
