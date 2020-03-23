using System.ComponentModel.DataAnnotations;

namespace Testovoe_zadaniye.Models
{
    public class GuideEditForm
    {
        [Required]
        [StringLength(30, MinimumLength = 5)]
        [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Only Alphabets and Numbers allowed.")]
        public string Login { get; set; }
        // Пароль Гида
        [Required]
        public string Password { get; set; }

        [StringLength(30, MinimumLength = 2)]
        [Required]
        [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Only Alphabets and Numbers allowed.")]
        // Имя Гида
        public string Name { get; set; }
        public int GuideId { get; set; }
    }
}
