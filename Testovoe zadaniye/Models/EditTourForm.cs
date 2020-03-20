using System;
using System.ComponentModel.DataAnnotations;


namespace Testovoe_zadaniye.Models
{
    public class EditTourForm
    {
        public int TourId { get; set; }

        [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$", ErrorMessage = "Only numbers and alphabets allowed, capital letter is compulsory, symbols only allow '-'")]
        public string Name { get; set; }
        public DateTime Date { get; set; }
    }
}
