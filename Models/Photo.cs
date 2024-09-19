using System.ComponentModel.DataAnnotations.Schema;

namespace ScheduleGym.Models
{
    public class Photo
    {
        public int Id { get; set; }
        public byte[] Bytes { get; set; }
        public string Description { get; set; }
        public string FileExtension { get; set; }
        public decimal Size { get; set; }
        public int PlaceId { get; set; }
        [ForeignKey("PlaceId")]
        public Place Place { get; set; }
    }
}
