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
using Testovoe_zadaniye.FileUploading;
using Microsoft.AspNetCore.Authorization;
using Testovoe_zadaniye.AppServices.Interfaces;

namespace Testovoe_zadaniye.Controllers
{
    public class TourController : Controller
    {
        TouragencyContext db;
        IWebHostEnvironment _appEnvironment;
        Logger logger;
        ITourService _tourService;

        public TourController(TouragencyContext context, IWebHostEnvironment appEnvironment, ITourService tourService)
        {
            db = context;
            LoggerCreator loggerCreator = new TxtLoggerCreator();
            logger = loggerCreator.FactoryMethod();
            _appEnvironment = appEnvironment;
            _tourService = tourService;

        }

        [Authorize]
        public IActionResult AddTour()
        {
            var result = _tourService;
            return View(result);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddTour(AddTour addTour)
        {
            await _tourService.SaveNewTour(addTour);

            string message = "Add of new Tour";
            logger.LoggMessage(this.GetType().Name, message);

            return RedirectToAction("GuideToToursAcces", "Navigation");
        }

        [Authorize]
        public ActionResult ToTourEdit(int id = 0)
        {
            Tour tour = new Tour();
            if (id != 0)
            {
                tour = db.Tours.Where(x => x.TourId == id).FirstOrDefault();
            }
            EditTourForm model = new EditTourForm();
            model.Date = tour.Data;
            model.Name = tour.Name;
            model.TourId = tour.TourId;


            return View(model);
        }

        [Authorize]
        [HttpPost]
        public ActionResult ToTourEdit(EditTourForm model)
        {
            Tour tour = new Tour();
            tour.Data = model.Date;
            tour.Name = model.Name;
            tour.TourId = model.TourId;

            if (model.TourId != 0)
            {
                db.Entry(tour).State = EntityState.Modified;
                db.SaveChanges();
            }
            string message = "Edit of chosen Tour";
            logger.LoggMessage(this.GetType().Name, message);

            return Ok();
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

        [HttpGet]
        [ActionName("DeleteTour")]
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> DeleteTour(int? id)
        {
            if (id != null)
            {
                var targetTour = db.Tours.First(x => x.TourId == id.Value); //Вынимаем тур по id из базы.
                db.Entry(targetTour).State = EntityState.Deleted; // удаляем тур
                await db.SaveChangesAsync();
                return RedirectToAction("GuideToToursAcces");
            }

            string message = "Deleting of Tour";
            string className = this.GetType().Name;

            logger.LoggMessage(className, message);

            return NotFound();
        }

        public IActionResult TourList()
        {

            string message = "Display tours List";
            string className = this.GetType().Name;

            logger.LoggMessage(className, message);

            return View(db.Tours.ToList());
        }
    }
}