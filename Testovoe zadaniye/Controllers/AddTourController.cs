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


namespace Testovoe_zadaniye.Controllers
{
    public class AddTourController : Controller
    {
        TouragencyContext db;
        IWebHostEnvironment _appEnvironment;
        Logger logger;

        public AddTourController(TouragencyContext context, IWebHostEnvironment appEnvironment)
        {
            db = context;
            LoggerCreator loggerCreator = new TxtLoggerCreator();
            logger = loggerCreator.FactoryMethod();
            _appEnvironment = appEnvironment;

        }
        public ActionResult ToTourAdd()
        {
            AddTour model = new AddTour();

            return View(model);
        }

        [HttpPost]
        public IActionResult ToTourAdd(AddTour addTour)
        {

            Tour tour = new Tour();
            tour.Name = addTour.Name;
            tour.Data = addTour.Date;
            if (tour.Name != null && tour.Data != null)
            {
            db.Tours.Add(tour);
            db.SaveChanges();
            }
            else
            {
                throw new Exception("Some field of tour is null");
            }


            string message = "Add of new Tour";
            logger.LoggMessage(this.GetType().Name, message);

            return RedirectToAction("GuideToToursAcces", "Navigation");
        }

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
    }
}