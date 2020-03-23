using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Testovoe_zadaniye.Models;

namespace Testovoe_zadaniye.Models.OperationModels
{
    public class GuidesAuthenticationForm
    {
        [Required(ErrorMessage = "Enter your login!")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Enter your password!")]
        public string Password { get; set; }

    }
}
