﻿using System;
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


// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Testovoe_zadaniye.Controllers
{
    public class AddTouristController : Controller
    {
        TouragencyContext db;
        IWebHostEnvironment _appEnvironment;
        Logger logger;

        public AddTouristController(TouragencyContext context, IWebHostEnvironment appEnvironment)
        {
            db = context;
            LoggerCreator loggerCreator = new TxtLoggerCreator();
            logger = loggerCreator.FactoryMethod();
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
                model.Path = await model.Avatar.PathReturn(_appEnvironment) ;
            }

            //model.Guides = db.Guides.ToList();
            //model.Tours = db.Tours.ToList();
            //model.selectListg = new SelectList(model.Guides, "GuideId", "Name");
            //model.selectListt = new MultiSelectList(model.Tours, "TourId", "Name");



            List<int> tours = model.Tours.Where(x => x.selected == true).Select(x => x.TourId).ToList();
            model.SelectedTourIds = tours;

            if (model.Avatar.Length > 1000000)
            {
                ModelState.AddModelError("Avatar", "File size bigger then 1Mb");
            }

            string message = "Upload new tourist form to submition";
            logger.LoggMessage(this.GetType().Name, message);

            if (ModelState.IsValid)
            return RedirectToAction("AddTouristform", model);


            return View(model);
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



            string message = "Submition of new tourist form";
            string className = this.GetType().Name;

            logger.LoggMessage(className, message);



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

            string message = "Saving of new tourist";
            string className = this.GetType().Name;

            logger.LoggMessage(className, message);

            return RedirectToAction("ToTouristList", "Navigation", new { id = tourist.GuideId });
        }

    }
}
