using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScheduleGym.Models.Commands
{
    public class ReviewCommand
    {
        public string Comment { get; set; }
        public int NumberOfStars { get; set; }
        public DateTime Date { get; set; }
        public int PlaceId { get; set; }
        public int UserId { get; set; }
    }
}