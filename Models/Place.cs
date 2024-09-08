using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ScheduleGym.Models.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ScheduleGym.Models
{
    public class Place
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Capacity { get; set; }
        public PlaceKind Kind { get; set; }
        public string  City { get; set; }
        public string Multiplicity { get; set; }
        public string  Address { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public double Rating { get; set; }
        [JsonIgnore]
        public User? Owner { get; set; }
        public int? OwnerId { get; set; }
        [JsonIgnore]
        public ICollection<AvalableTerms>? avalableTerms { get; } = new List<AvalableTerms>();
        [JsonIgnore]
        public List<Appointments>? appointments { get; } = new List<Appointments>();
        [JsonIgnore]
        public ICollection<Review>? reviewes { get; } = new List<Review>(); 


        public Place(){}


    }
}