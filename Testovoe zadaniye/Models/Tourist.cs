using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Testovoe_zadaniye.Models
{
    public class Tourist
    {
        [Key]
        //id туриста
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Touristid { get; set; }
        // Родной город туриста
        [MaxLength(30)]
        [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$")]
        [Required]
        public string Hometown { get; set; }
     
        // Полное имя туриста
        public string Fullname { get; set; }
        [Required]
        public int Age { get; set; }
        [Required]
        public string Avatar { get; set; }
        public int GuideId { get; set; }
        public Guide Guide { get; set; }
        public ICollection<TouristTour> TouristTours { get; set; }
    }
}
