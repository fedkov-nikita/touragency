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
    public class Addform
    {
        // Родной город туриста
        [StringLength(40, MinimumLength = 2)]
        [Required]
        [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$")]
        public string Hometown { get; set; }
        [StringLength(40, MinimumLength = 4)]
        // Полное имя туриста
        [Display(Name = "Full Name")]
        [Required]
        public string Fullname { get; set; }
        [Required]
        [Range(6, 150)]
        // Возраст туриста
        public int Age { get; set; }
        // Логин Гида
        [Display(Name = "Guide")]
        public int GuideId { get; set; }
        public int TourId { get; set; }
        public string Path { get; set; }
        [Required]
        public IFormFile Avatar { get; set; }
        // Название экскурсии
        public List<Tour> Tours { get; set; }
        public List<int> SelectedTourIds { get; set; }
        public List<Guide> Guides { get; set; }
        public SelectList selectListg { get; set; }
        public MultiSelectList selectListt { get; set; }
    }
}
