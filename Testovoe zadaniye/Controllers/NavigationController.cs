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


namespace Testovoe_zadaniye.Controllers
{
    public class NavigationController : Controller
    {
        TouragencyContext db;
        public NavigationController(TouragencyContext context)
        {
            db = context;
        }
        public ActionResult GuideSelection()
        {
            return View();
        }
        public IActionResult ToGuide(int? id)
        {
            var viewModel = new TouristindexData();
            return View(db.Guides.Where(
                        i => i.GuideId == id.Value).ToList());
        }
        public ActionResult ToGuides()
        {
            return View(db.Guides.ToList());
        }
        public IActionResult ToTouristList(int? id)
        {
            return View(db.Guides.Include(x => x.Tourists).First(
                        i => i.GuideId == id.Value).Tourists);
        }
        public IActionResult ToTours(int? id)
        {
            var viewModel = new TouristindexData();
            return View(db.TouristTour.Where(
                        i => i.TouristId == id.Value).Select(i => i.Tour).ToList());
        }
        public IActionResult TouristList()
        {
            return View(db.Tourists.ToList());
        }
        [HttpGet]
        [ActionName("Delete")]

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                var targetTourist = db.Tourists.First(x => x.Touristid == id.Value); //Вынимаем турста по id из базы.
                int temp = targetTourist.GuideId; // сохраняем айди гида
                db.Entry(targetTourist).State = EntityState.Deleted; // удаляем туриста
                await db.SaveChangesAsync();
                return RedirectToAction("ToTouristList",new { id = temp });
            }
            return NotFound();
        }
        [HttpGet]
        public ActionResult ToEdit(int id = 0)
        {
            Tourist tourist = new Tourist();
            if (id != 0)
            {
                    tourist = db.Tourists.Where(x => x.Touristid == id).FirstOrDefault();
            }
            EditForm model = new EditForm();
            model.Hometown = tourist.Hometown;
            model.Fullname = tourist.Fullname;
            model.Age = tourist.Age;
            model.GuideId = tourist.GuideId;
            model.UploadedPhoto = tourist.Avatar;
            model.Tours = db.Tours.ToList();
            model.selectListt = new MultiSelectList(model.Tours, "TourId", "Name");
            model.SelectedTourIds = db.TouristTour.Where(x => x.TouristId == tourist.Touristid).Select(x => x.TourId).ToList();
            model.Guides = db.Guides.ToList();
            model.selectListg = new SelectList(model.Guides, "GuideId", "Name");
            model.Touristid = tourist.Touristid;
            foreach (var t in model.Tours)
            {
                if (model.SelectedTourIds.Contains(t.TourId))
                    t.selected = true;
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult ToEdit(EditForm model)
        {
            Tourist tourist = new Tourist();
            tourist.Fullname = model.Fullname;
            tourist.Hometown = model.Hometown;
            tourist.GuideId = model.GuideId;
            tourist.Age = model.Age;
            tourist.Touristid = model.Touristid;
            tourist.Avatar = model.UploadedPhoto;
            if (model.Avatar != null)
            {
                byte[] imageData = null;
                // считываем переданный файл в массив байтов
                using (var binaryReader = new BinaryReader(model.Avatar.OpenReadStream()))
                {
                    imageData = binaryReader.ReadBytes((int)model.Avatar.Length);
                }

                // установка массива байтов
                tourist.Avatar = imageData;
            }
            
            List<TouristTour> touristToursToRemove = new List<TouristTour>();
            touristToursToRemove = db.TouristTour.Where(x => x.TouristId == tourist.Touristid).ToList();
            db.RemoveRange(touristToursToRemove);
            db.SaveChanges();

            List<TouristTour> touristToursToAdd = new List<TouristTour>();
            foreach (var t in model.Tours.Where(x => x.selected == true))
            {
                TouristTour touristTour = new TouristTour();
                touristTour.TouristId = tourist.Touristid;
                touristTour.TourId = t.TourId;
                touristToursToAdd.Add(touristTour);
            }
            db.TouristTour.AddRange(touristToursToAdd);
            db.SaveChanges();

            if (model.Touristid != 0)
                    {
                        db.Entry(tourist).State = EntityState.Modified;
                        db.SaveChanges();
                    }
            //return RedirectToAction("ToTouristList", "Navigation", new { id = tourist.GuideId });
            return Ok();
            
            
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


    }
}
