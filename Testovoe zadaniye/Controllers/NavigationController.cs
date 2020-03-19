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
using Microsoft.AspNetCore.Http;
using Testovoe_zadaniye.Paginator;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Authorization;
using ITouristService = Testovoe_zadaniye.AppServices.Interfaces.ITouristService;
using Testovoe_zadaniye.AppServices.Interfaces;

namespace Testovoe_zadaniye.Controllers
{
   
    public class NavigationController : Controller
    {
        TouragencyContext db;
        IWebHostEnvironment _appEnvironment;
        Logger logger;
        IFileManager _fileManager;
        ITouristService _touristServ;

        public NavigationController(TouragencyContext context, IWebHostEnvironment appEnvironment, ITouristService touristServ, IFileManager fileManager)
        {
            db = context;
            LoggerCreator loggerCreator = new TxtLoggerCreator();
            logger = loggerCreator.FactoryMethod();
            _appEnvironment = appEnvironment;
            _fileManager = fileManager;
            _touristServ = touristServ;

        }
        [Authorize]
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

        [Authorize]
        public ActionResult ToGuides()
        {

            string message = "Display guides list";
            string className = this.GetType().Name;

            logger.LoggMessage(className, message);

            return View(db.Guides.ToList());
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

        [HttpGet]
        [ActionName("DeleteGuide")]
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> DeleteGuide(int? id)
        {
            if (id != null)
            {
                var targetGuide = db.Guides.First(x => x.GuideId == id.Value); //Вынимаем тур по id из базы.
                db.Entry(targetGuide).State = EntityState.Deleted; // удаляем тур
                await db.SaveChangesAsync();
                return RedirectToAction("ToGuides");
            }

            string message = "Deleting of Guide";
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

        [Authorize]
        public IActionResult GuideToToursAcces(int pageNumber = 1, int pageSize = 2)
        {
            int ExcludeRecords = (pageNumber * pageSize) - pageSize; 
            string message = "Display tours List for guide";
            string className = this.GetType().Name;

            logger.LoggMessage(className, message);

            var result = new Pagin<Tour>
            {
                Data = db.Tours.OrderBy(c => c.Name).Skip(ExcludeRecords).Take(pageSize).AsNoTracking().ToList(),
                TotalItems = db.Tours.Count(),
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            return View(result);
        }

        
       

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
