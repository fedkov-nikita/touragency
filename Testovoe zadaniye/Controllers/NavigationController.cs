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
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
