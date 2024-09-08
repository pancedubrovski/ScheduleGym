using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ScheduleGym.Models.Enums;
using System.Text.Json.Serialization;


namespace ScheduleGym.Models.Commands
{
    public class RegisterCommand
    {
        
        public string Username { get; set; }

        public string Password { get; set; }

        public string FullName { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }
        public string Role { get; set; }
    }
}