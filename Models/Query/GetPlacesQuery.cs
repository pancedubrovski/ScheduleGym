using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ScheduleGym.Models.Enums;

namespace ScheduleGym.Models.Query
{
    public class GetPlacesQuery : PaginationQuery
    {
        public string? Date { get; set; }
        public string? StartTime { get; set; }
        public string? EndTime { get; set; }
        public string? City { get; set; }
        public PlaceKind? Kind { get; set; }

        public bool GetFreeGyms { get; set; }
        public string? Includes { get; set; }


    }
}