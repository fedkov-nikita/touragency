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


// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Testovoe_zadaniye.Controllers
{
    public class ToToursController : Controller
    {
        TouragencyContext db;
        public ToToursController(TouragencyContext context)
        {
            db = context;
        }
        public IActionResult ToTours(int? id)
        {
            var viewModel = new TouristindexData();
            return View(db.TouristTour.Where(
                        i => i.TouristId == id.Value).Select(i => i.Tour).ToList());
        }

    }
}
