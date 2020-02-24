using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Testovoe_zadaniye.Models;

namespace Testovoe_zadaniye.Models
{
    public class GuideLogin
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Password { get; set; }

    }
}
