using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Testovoe_zadaniye.Models;
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
        public string Name { get; set; }
        // Пароль Гида
        public DateTime Data { get; set; }
        public ICollection<TouristTour> TouristTours { get; set; }
        [NotMapped]
        public bool selected { get; set; }
    }
}
