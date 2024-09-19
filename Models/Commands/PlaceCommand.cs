using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ScheduleGym.Models.Enums;
using System.Text.Json.Serialization;
using System.Security.Cryptography.Xml;
using AutoMapper;



namespace ScheduleGym.Models.Commands
{
    public class PlaceCommand
    {
        public string Name { get; set; }
        public int Capacity { get; set; }
        public PlaceKind Kind { get; set; }
        public string  City { get; set; }


        [JsonPropertyName("multiplicity")]
        public string Multiplicity { get; set; }
        public string  Address { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }

        [JsonPropertyName("ownerId")]
        public string OwnerId { get; set; }
        [JsonPropertyName("photos")]
        [IgnoreMap]
        public List<IFormFile> Photos { get; set; }

        [JsonPropertyName("avalable-terms")]
        public List<AvailableTermCommand> AvalableTerms { get; set; } = new List<AvailableTermCommand>();

    }
}