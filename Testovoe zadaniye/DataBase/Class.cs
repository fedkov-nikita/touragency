using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Testovoe_zadaniye.DataBase
{
    public class Addform
    {
        // Родной город туриста
        public string Hometown { get; set; }
        // Полное имя туриста
        public string Fullname { get; set; }
        // Возраст туриста
        public int Age { get; set; }
        // Логин Гида
        public int GuideId { get; set; }
        // Имя Гида
        public int TourId { get; set; }
        // Название экскурсии
        public string Name { get; set; }
        // Пароль Гида
        public List<Tour> Tours { get; set; }
        public List<int> SelectedTourIds { get; set; }
    }
    public class Tourist
    {
        //id туриста
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Touristid { get; set; }
        // Родной город туриста
        public string Hometown { get; set; }
        // Полное имя туриста
        public string Fullname { get; set; }
        // Возраст туриста
        public int Age { get; set; }
        public int GuideId { get; set; }
        public Guide Guide { get; set; }
        public ICollection<TouristTour> TouristTours { get; set; }
    }
    public class Guide
    {
        //id Гида
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int GuideId { get; set; }
        // Логин Гида
        public string Login { get; set; }
        // Пароль Гида
        public string Password { get; set; }
        // Имя Гида
        public string Name { get; set; }
        public ICollection<Tourist> Tourists { get; set; }
    }
    public class Tour
    {
        //id экскурсии
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TourId { get; set; }
        // Название экскурсии
        public string Name { get; set; }
        // Пароль Гида
        public DateTime Data { get; set; }
        public ICollection<TouristTour> TouristTours { get; set; }
        [NotMapped]
        public bool selected { get; set; }
    }
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
