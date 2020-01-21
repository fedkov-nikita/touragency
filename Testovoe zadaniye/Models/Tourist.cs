using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Testovoe_zadaniye.Models;

namespace Testovoe_zadaniye.Models
{
    public class Tourist
    {
        //id туриста
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Touristid { get; set; }
        // Родной город туриста
        public string Hometown { get; set; }
        // Полное имя туриста
        public string Fullname { get; set; }
        // Возраст туриста
        public int Age { get; set; }
        public byte[] Avatar { get; set; }
        public int GuideId { get; set; }
        public Guide Guide { get; set; }
        public ICollection<TouristTour> TouristTours { get; set; }
    }
}
