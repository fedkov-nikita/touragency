using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Testovoe_zadaniye.Models
{
    public class EditGuideForm
    {
        [Required]
        public string Login { get; set; }
        // Пароль Гида
        public string Password { get; set; }
        [StringLength(60, MinimumLength = 2)]
        [Required]
        [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$")]
        // Имя Гида
        public string Name { get; set; }
        public int GuideId { get; set; }
    }
}
