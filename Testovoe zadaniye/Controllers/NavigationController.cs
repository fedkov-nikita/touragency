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
using Testovoe_zadaniye.LoggingMechanism;
using Testovoe_zadaniye.FileUploading;


namespace Testovoe_zadaniye.Controllers
{
    public class NavigationController : Controller
    {
        TouragencyContext db;
        IWebHostEnvironment _appEnvironment;
        Logger logger;

        public NavigationController(TouragencyContext context, IWebHostEnvironment appEnvironment)
        {
            db = context;
            LoggerCreator loggerCreator = new TxtLoggerCreator();
            logger = loggerCreator.FactoryMethod();
            _appEnvironment = appEnvironment;
        }
        public ActionResult GuideSelection()
        {

            string message = "Select of proper guide option";
            string className = this.GetType().Name;

            logger.LoggMessage(className, message);

            return View();
        }
        public IActionResult ToGuide(int? id)
        {

            string message = "Display tourist's guide";
            string className = this.GetType().Name;

            logger.LoggMessage(className, message);

            return View(db.Guides.Where(
                        i => i.GuideId == id.Value).ToList());
        }
        public ActionResult ToGuides()
        {

            string message = "Display guides list";
            string className = this.GetType().Name;

            logger.LoggMessage(className, message);

            return View(db.Guides.ToList());
        }
        public IActionResult ToTouristList(int? id)
        {

            var tourists = db.Tourists.FromSqlRaw("sp_ShowGuidesByTourist @GuideId={0}", id).ToList();

            //return View(db.Guides.Include(x => x.Tourists).First(
            //            i => i.GuideId == id.Value).Tourists);

            string message = "Display guide`s tourists";
            string className = this.GetType().Name;

            logger.LoggMessage(className, message);

            return View(tourists);
        }
        public IActionResult ToTours(int? id)
        {

            var viewModel = new TouristindexData();

            var tours = db.Tours.FromSqlRaw("sp_ShowToursFromTT @TouristId={0}", id).ToList();

            //return View(db.TouristTour.Where(
            //            i => i.TouristId == id.Value).Select(i => i.Tour).ToList());

            string message = "Display tours";
            string className = this.GetType().Name;

            logger.LoggMessage(className, message);

            return View(tours);
        }
        public IActionResult TouristList()
        {

            string message = "Display tourists list";
            string className = this.GetType().Name;

            logger.LoggMessage(className, message);

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
                return RedirectToAction("ToTouristList", new { id = temp });
            }

            string message = "Deleting of tourist";
            string className = this.GetType().Name;

            logger.LoggMessage(className, message);

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
            model.Path = tourist.Avatar;
            model.Tours = db.Tours.ToList();
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
        public async Task<ActionResult> ToEditAsync(EditForm model)
        {
            Tourist tourist = new Tourist();
            tourist.Fullname = model.Fullname;
            tourist.Hometown = model.Hometown;
            tourist.GuideId = model.GuideId;
            tourist.Age = model.Age;
            tourist.Touristid = model.Touristid;

            if (model.Avatar != null)
            {
                model.Path = await model.Avatar.PathReturn(_appEnvironment);
            }
            tourist.Avatar = model.Path;

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

            string message = "Editing od tourist";
            string className = this.GetType().Name;

            logger.LoggMessage(className, message);

            return Ok();


        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


    }
}
