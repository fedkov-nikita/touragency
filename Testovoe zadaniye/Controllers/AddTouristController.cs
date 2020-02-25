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
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.Runtime.CompilerServices;
using Testovoe_zadaniye.LoggingMechanism;


// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Testovoe_zadaniye.Controllers
{
    public class AddTouristController : Controller
    {
        TouragencyContext db;
        IWebHostEnvironment _appEnvironment;

        public AddTouristController(TouragencyContext context, IWebHostEnvironment appEnvironment)
        {
            db = context;
            _appEnvironment = appEnvironment;
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
        public async Task<ActionResult> AddTourist(Addform model)
        {
            if (model.Avatar != null)
            {
                // путь к папке Files
                var d = Directory.CreateDirectory(@"E:\TestovoeZadanie\Testovoe zadaniye\Testovoe zadaniye\wwwroot\images");
                string path = d + model.Avatar.FileName;
                // сохраняем файл в папку Files в каталоге wwwroot
                string p = path;
                string fileName = Path.GetFileName(p);
                string fileExtension = Path.GetExtension(fileName);
                string randomFileName = Path.GetRandomFileName();
                string fullPath = "/images/" + Path.GetFileNameWithoutExtension(randomFileName) + fileExtension;
                model.Path = fullPath;
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + fullPath, FileMode.Create))
                {
                    await model.Avatar.CopyToAsync(fileStream);
                }

            }
            List<int> tours = model.Tours.Where(x => x.selected == true).Select(x => x.TourId).ToList();
            model.SelectedTourIds = tours;

            Logging logging = new Logging();
            string methName =  logging.DefineMethodName();
            logging.LoggMassage(methName);

            return RedirectToAction("AddTouristform", model);
        }
        
        public ActionResult AddTouristform(Addform model)
        {
            
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


            Logging logging = new Logging();
            string methName = logging.DefineMethodName();
            logging.LoggMassage(methName);

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
            tourist.Avatar = model.Path;
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

            Logging logging = new Logging();
            string methName = logging.DefineMethodName();
            logging.LoggMassage(methName);


            string className = this.GetType().Name;
            logging.LoggMassageClass(className);

            return RedirectToAction("ToTouristList", "Navigation", new { id = tourist.GuideId });
        }
        //public string SomeMethod([CallerMemberName] string memberName = "")
        //{
        //    var a = memberName;
        //    return a;
        //}


    }
}
