using System.ComponentModel.DataAnnotations;

namespace IntelRobotics.Models
{
    public class KontaktForm
    {
        [Key]
        public int Id { get; set; }
        [Display(Name ="Name")]
        public string Name { get; set; }
        [Display(Name = "CompanyName")]
        public string CompanyName { get; set; }
        [Display(Name ="Email")]
        public string Email { get; set; }
        [Display(Name = "Regarding")]
        public string Regarding { get; set; }
        public DateTime RequestDate { get; set; }
        public List<KontaktFormToRobot>? robot { get; set; }
    }
}
