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
        public ActionResult EditTour(int id = 0)
        {
            var result = _tourService.CreateEditTourForm(id);
            return View(result);
        }

        [Authorize]
        [HttpPost]
        public ActionResult EditTour(EditTourForm model)
        {
            _tourService.SaveEditedTour(model);

            string message = "Edit of chosen Tour";
            logger.LoggMessage(this.GetType().Name, message);

            return Ok();
        }

        public IActionResult ToTours(int? id)
        {
            string message = "Display tours";
            string className = this.GetType().Name;

            logger.LoggMessage(className, message);

            //SQL useful realisation
            //var tours = db.Tours.FromSqlRaw("sp_ShowToursFromTT @TouristId={0}", id).ToList();
            //return View(tours);

            var tours = _tourService.ToursOfChosenTourist(id);

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
                string message = "Deleting of Tour";
                string className = this.GetType().Name;
                logger.LoggMessage(className, message);

                await _tourService.DeleteChosenTour(id);
                return RedirectToAction("AllToursForGuide");
            }

            string message1 = "Unsucessful tour delete";
            string className1 = this.GetType().Name;
            logger.LoggMessage(className1, message1);

            return NotFound();
        }

        public IActionResult AllToursList()
        {

            string message = "Display tours List";
            string className = this.GetType().Name;
            logger.LoggMessage(className, message);

            var result = _tourService.ShowAllTours();

            return View(result);
        }
    }
}