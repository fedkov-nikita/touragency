using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Testovoe_zadaniye.Models
{
    public class AddGuide
    {
        [Required]
        [StringLength(30, MinimumLength = 5)]
        [RegularExpression("^[a-zA-Z0]*$", ErrorMessage = "Only Alphabets and Numbers allowed.")]
        public string Login { get; set; }
        // Пароль Гида
        [Required]
        [StringLength(30, MinimumLength = 5, ErrorMessage = " 5 - 30 symbols required ")]
        public string Password { get; set; }

        [StringLength(30, MinimumLength = 2)]
        [Required]
        // Имя Гида
        public string Name { get; set; }
    }
}
