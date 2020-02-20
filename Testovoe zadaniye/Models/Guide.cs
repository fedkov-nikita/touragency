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
    public class Guide
    {
        [Key]
        //id Гида
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int GuideId { get; set; }
        // Логин Гида
        public string Login { get; set; }
        // Пароль Гида
        public string Password { get; set; }
        // Имя Гида
        public string Name { get; set; }
        [NotMapped]
        public bool selected { get; set; }
        public ICollection<Tourist> Tourists { get; set; }
    }
}
