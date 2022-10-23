using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IntelRobotics.Models
{
    public class Robot
    {
        [Key]
        public Guid Robotid { get; set; }
        [Column(TypeName ="varchar(50)")]
        [Display(Name ="Navn")]
        public string Name { get; set; }

        [Display(Name ="Beskrivelse")]
        public string Description { get; set; }
        [Display(Name ="Instock")]
        public bool available { get; set; }
        [Display(Name ="Pris")]
        public float Price { get; set; }
        [Column(TypeName ="nvarchar(50)")]

        public string? ImageName { get; set; }
        [NotMapped]
        [Display(Name ="Upload file")]
        public IFormFile? ImageFile { get; set; }
        [Display(Name = "Lifting capacity")]
        public string? LigtingCapacity { get; set; }
        [Display(Name = "Weight")]
        public string? Weight { get; set; }
        [Display(Name ="Footprint")]
        public string? Footprint { get; set; }
        [Display(Name ="Radius")]
        public string? Radius { get; set; }
        [Display(Name ="Parts")]
        public int? Parts { get; set; }

        public DateTime? Edit { get; set; }
    }
}
