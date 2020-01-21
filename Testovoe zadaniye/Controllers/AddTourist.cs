using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Testovoe_zadaniye.DataBase;
using Testovoe_zadaniye.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.IO;


// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Testovoe_zadaniye.Controllers
{
    public class AddTouristController : Controller
    {
        TouragencyContext db;
        public AddTouristController(TouragencyContext context)
        {
            db = context;
        }
        public ActionResult AddTourist()
        {

            Addform model = new Addform();
            model.Guides = db.Guides.ToList();
            model.Tours = db.Tours.ToList();
            model.selectListg = new SelectList(model.Guides, "GuideId", "Name");
            model.selectListt = new MultiSelectList(model.Tours, "TourId", "Name");
            return View(model);
        }

        [HttpPost]
        public ActionResult AddTourist(Addform model)
        {
            if (model.Avatar != null)
            {
                byte[] imageData = null;
                // считываем переданный файл в массив байтов
                using (var binaryReader = new BinaryReader(model.Avatar.OpenReadStream()))
                {
                    imageData = binaryReader.ReadBytes((int)model.Avatar.Length);
                }

                // установка массива байтов
                temp = imageData;
            }
            List<int> tours = model.Tours.Where(x => x.selected == true).Select(x => x.TourId).ToList();
            model.SelectedTourIds = tours;
            return RedirectToAction("AddTouristform", model);
        }
        static byte[] temp = null;
        public ActionResult AddTouristform(Addform model)
        {
            ViewBag.UploadedPhoto = temp;
            model.Tours = db.Tours.ToList();
            foreach (var t in model.Tours)
            {
                if (model.SelectedTourIds.Contains(t.TourId))
                    t.selected = true;
            }
            model.Guides = db.Guides.ToList();
            model.selectListg = new SelectList(model.Guides, "GuideId", "Name");
            var selectedGuideItem = model.selectListg.First(x => x.Value == model.GuideId.ToString());
            selectedGuideItem.Selected = true;
            return View(model);
        }

        [HttpPost]
        public ActionResult SaveNewTourist(Addform model)
        {
            foreach (var t in model.Tours)
            {
                if (model.SelectedTourIds.Contains(t.TourId))
                    t.selected = true;
            }
            Tourist tourist = new Tourist();
            tourist.Fullname = model.Fullname;
            tourist.Hometown = model.Hometown;
            tourist.GuideId = model.GuideId;
            tourist.Age = model.Age;
            tourist.Avatar = temp;
            List<TouristTour> touristTours = new List<TouristTour>();
            db.Tourists.Add(tourist);
            // сохраняем в бд все изменения
            db.SaveChanges();
            foreach (var t in model.Tours.Where(x => x.selected == true))
            {
                TouristTour touristTour = new TouristTour();
                touristTour.TouristId = tourist.Touristid;
                touristTour.TourId = t.TourId;
                touristTours.Add(touristTour);
            }
            db.TouristTour.AddRange(touristTours);
            db.SaveChanges();
            return RedirectToAction("ToTouristList", "ToTouristList", new { id = tourist.GuideId });
        }
    }
}
