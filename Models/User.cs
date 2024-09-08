using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ScheduleGym.Models.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace ScheduleGym.Models
{
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Username { get; set; }
        public string PasswordSalt { get; set; }
        public string PasswordHash { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public Role Role { get; set; }
        [JsonIgnore]
        public ICollection<Place>? owners { get; } = new List<Place>(); 
        public CustomerPreferences CustomerPreference { get; }
        [JsonIgnore]
        public ICollection<Appointments>? appointments { get; } = new List<Appointments>(); 
        
    }
}