using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Testovoe_zadaniye.Models
{
    public class Tour
    {
        [Key]
        //id экскурсии
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TourId { get; set; }
        // Название экскурсии
        [RegularExpression(@"^[A-Z]+[a-zA-Z0-9""'\s-]*$")]
        [StringLength(30)]
        [Required]
        public string Name { get; set; }
        // Пароль Гида
        [DataType(DataType.Date)]
        public DateTime Data { get; set; }
        public ICollection<TouristTour> TouristTours { get; set; }
        [NotMapped]
        public bool selected { get; set; }

    }
}
