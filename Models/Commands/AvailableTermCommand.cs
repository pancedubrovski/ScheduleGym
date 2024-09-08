using System;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;



namespace ScheduleGym.Models.Commands
{
    public class AvailableTermCommand
    {
        [JsonPropertyName("days-of-week")]
        public string DaysOfWeek { get; set; }

        [JsonPropertyName("start-time")]
        public string StartTime { get; set; }

        [JsonPropertyName("end-time")]
        public string EndTime { get; set; }
    }
}