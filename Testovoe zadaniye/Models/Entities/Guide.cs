using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Testovoe_zadaniye.Models.Entities
{
    public class Guide
    {
        [Key]
        //id Гида
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int GuideId { get; set; }
        // Логин Гида
        [Required]
        public string Login { get; set; }
        // Пароль Гида
        public string Password { get; set; }
        [StringLength(30, MinimumLength = 2)]
        [Required]
        // Имя Гида
        public string Name { get; set; }
        [NotMapped]
        public bool selected { get; set; }
        public ICollection<Tourist> Tourists { get; set; }
    }
}
