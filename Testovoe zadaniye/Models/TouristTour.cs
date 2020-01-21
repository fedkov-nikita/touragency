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
    public class TouristTour
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int TouristId { get; set; }
        public Tourist Tourist { get; set; }
        public int TourId { get; set; }
        public Tour Tour { get; set; }

    }
}
