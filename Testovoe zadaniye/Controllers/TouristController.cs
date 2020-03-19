﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Testovoe_zadaniye.DataBase;
using Testovoe_zadaniye.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Hosting;
using Testovoe_zadaniye.LoggingMechanism;
using Testovoe_zadaniye.FileUploading;
using Microsoft.AspNetCore.Authorization;
using ITouristService = Testovoe_zadaniye.AppServices.Interfaces.ITouristService;
using Microsoft.EntityFrameworkCore;


// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Testovoe_zadaniye.Controllers
{
    public class TouristController : Controller
    {
        ITouristService _touristServ;
        TouragencyContext db;
        IWebHostEnvironment _appEnvironment;
        Logger logger;

        public TouristController(TouragencyContext context, IWebHostEnvironment appEnvironment, ITouristService touristServ)
        {
            db = context;
            LoggerCreator loggerCreator = new TxtLoggerCreator();
            logger = loggerCreator.FactoryMethod();
            _appEnvironment = appEnvironment;
            _touristServ = touristServ;

        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult> AddTouristFormStep1()
        {
            var result = await _touristServ.CreateNewTouristForm();
            return View(result);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> AddTouristFormStep1(Addform model)
        {
            await _touristServ.ProcessNewTouristModel(model);

            string message = "Upload new tourist form to submition";
            logger.LoggMessage(this.GetType().Name, message);

            if (ModelState.IsValid)
                return RedirectToAction("AddTouristFormStep2", model);

            return View(model);
        }

        [Authorize]
        public async Task<ActionResult> AddTouristFormStep2(Addform model)
        {
            await _touristServ.ProcessNewTouristModel(model);

            string message = "Submition of new tourist form";
            string className = this.GetType().Name;

            logger.LoggMessage(className, message);

            return View(model);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> SaveNewTourist(Addform model)
        {

            var result =  await _touristServ.SaveTouristModel(model);

            string message = "Saving of new tourist";
            string className = this.GetType().Name;

            logger.LoggMessage(className, message);

            return RedirectToAction("TouristsOfCurrentGuideForm", "Tourist", new { id = result.GuideId });
        }

        [Authorize]
        public async Task<IActionResult> TouristsOfCurrentGuideForm(int? id)
        {
            //SQL Request Example
            //var tourists = db.Tourists.FromSqlRaw("sp_ShowGuidesByTourist @GuideId={0}", id).ToList();
            //return View(tourists);

            string message = "Display guide`s tourists";
            string className = this.GetType().Name;

            logger.LoggMessage(className, message);

            return View(await _touristServ.TouristListById(id));
        }

        public async Task<IActionResult> TouristList(int? age, string homeTown, string searchString, int pageNumber = 1, int pageSize = 7)
        {

            string message = "Display tourists list";
            string className = this.GetType().Name;

            logger.LoggMessage(className, message);

            var result = await _touristServ.FullTouristList(age, homeTown, searchString, pageNumber, pageSize);

            return View(result);
        }

        [HttpGet]
        [ActionName("Delete")]
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> DeleteTourist(int? id)
        {
            if (id != null)
            {
                var targetTourist = db.Tourists.First(x => x.Touristid == id.Value); //Вынимаем турста по id из базы.
                int temp = targetTourist.GuideId; // сохраняем айди гида
                db.Entry(targetTourist).State = EntityState.Deleted; // удаляем туриста
                await db.SaveChangesAsync();
                return RedirectToAction("TouristsOfCurrentGuideForm", new { id = temp });
            }

            string message = "Deleting of tourist";
            string className = this.GetType().Name;

            logger.LoggMessage(className, message);

            return NotFound();
        }

    }
}