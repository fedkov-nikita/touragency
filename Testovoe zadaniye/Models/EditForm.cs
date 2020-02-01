﻿using Microsoft.AspNetCore.Mvc.Rendering;
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
    public class EditForm
    {
        public byte[] UploadedPhoto { get; set; }
        // Родной город туриста
        public string Hometown { get; set; }
        // Полное имя туриста
        [Display(Name = "Full Name")]
        public string Fullname { get; set; }
        // Возраст туриста
        public int Age { get; set; }
        // Логин Гида
        [Display(Name = "Guide")]
        public int GuideId { get; set; }
        public int TourId { get; set; }
        //Id Туриста
        public int Touristid { get; set; }
        public IFormFile Avatar { get; set; }
        // Название экскурсии
        public string Name { get; set; }
        public List<Tour> Tours { get; set; }
        public List<int> SelectedTourIds { get; set; }
        public List<Guide> Guides { get; set; }
        public SelectList selectListg { get; set; }
        public MultiSelectList selectListt { get; set; }
    }
}