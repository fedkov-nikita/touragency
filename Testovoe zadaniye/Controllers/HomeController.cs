using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Testovoe_zadaniye.DataBase;
using Testovoe_zadaniye.Models;
using Microsoft.EntityFrameworkCore;

namespace Testovoe_zadaniye.Controllers
{
    public class HomeController : Controller
    {
        
        TouragencyContext db;
        public HomeController(TouragencyContext context)
        {
            db = context; 
        }
        public ActionResult Index()
        {
            return View();
        }
        private TouragencyContext _context;

        public ActionResult AddTourist()
        {
            List<Guide> li = new List<Guide>();
            li = db.Guides.ToList();
            ViewBag.listofitems = li;
            Addform model = new Addform();
            model.Tours = db.Tours.ToList();
            return View(model);
        }

        [HttpPost]
        public ActionResult AddTourist(Addform model)
        {
            List<int> tours = model.Tours.Where(x => x.selected == true).Select(x => x.TourId).ToList();
            model.SelectedTourIds=tours;
            return RedirectToAction("AddTouristform", model);
        }

        public ActionResult AddTouristform(Addform model)
        {
            model.Tours = db.Tours.ToList();
                foreach(var t in model.Tours)
            {
                if (model.SelectedTourIds.Contains(t.TourId))
                    t.selected = true;         
            }
            List<Guide> li = new List<Guide>();
            li = db.Guides.ToList();
            ViewBag.listofitems = li;
            return View(model);
        }

        [HttpPost]
        public ActionResult GuideLog(GuideLogin guideLogin)
        {
            if (guideLogin.Name == null && guideLogin.Password == null) return RedirectToAction("Index");
            var g = db.Guides.Any(x=>x.Login == guideLogin.Name && x.Password == guideLogin.Password);
            if (g == true)
            {
                return RedirectToAction("GuideSelection");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
        [HttpGet]
        public ActionResult GuideLog()
        {
            return View();
        }
        public ActionResult GuideSelection()
        {
            return View();
        }
        public ActionResult ToGuides()
        {
            return View(db.Guides.ToList());
        }
        public IActionResult TouristList()
        {
            return View(db.Tourists.ToList());
        }
        public IActionResult ToTouristList(int? id)
        {
            return View(db.Guides.Include(x=>x.Tourists).First(
                        i => i.GuideId == id.Value).Tourists);
        }
        public IActionResult ToGuide(int? id)
        {
            var viewModel = new TouristindexData();
            return View(db.Guides.Where(
                        i => i.GuideId == id.Value).ToList());
        }
        public IActionResult ToTours(int? id)
        {
            var viewModel = new TouristindexData();
            return View(db.TouristTour.Where(
                        i => i.TouristId == id.Value).Select(i=>i.Tour).ToList());
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
