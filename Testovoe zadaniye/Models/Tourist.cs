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
    public class Tourist
    {
        [Key]
        //id туриста
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Touristid { get; set; }
        // Родной город туриста
        [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$")]
        [Required]
        public string Hometown { get; set; }
        [Required]
        // Полное имя туриста
        public string Fullname { get; set; }
        // Возраст туриста
        public int Age { get; set; }
        [Required]
        public string Avatar { get; set; }
        public int GuideId { get; set; }
        public Guide Guide { get; set; }
        public ICollection<TouristTour> TouristTours { get; set; }
    }
}
