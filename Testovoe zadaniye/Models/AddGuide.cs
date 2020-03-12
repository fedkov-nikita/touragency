﻿using System;
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
        [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Only Alphabets and Numbers allowed.")]
        public string Login { get; set; }
        // Пароль Гида
        [Required]
        [StringLength(30, MinimumLength = 5)]
        [RegularExpression("^[A-Z]-[a-zA-Z0-9]*$", ErrorMessage = "Only numbers and alphabets allowed, capital letter is compulsory ")]
        public string Password { get; set; }

        [StringLength(30, MinimumLength = 2)]
        [Required]
        [RegularExpression("^[A-Z]-[a-zA-Z0-9]*$", ErrorMessage = "Only numbers and alphabets allowed, capital letter is compulsory ")]
        // Имя Гида
        public string Name { get; set; }
    }
}
