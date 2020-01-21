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
    public class ToTouristListController : Controller
    {
        TouragencyContext db;
        public ToTouristListController(TouragencyContext context)
        {
            db = context;
        }
        public IActionResult ToTouristList(int? id)
        {
            return View(db.Guides.Include(x => x.Tourists).First(
                        i => i.GuideId == id.Value).Tourists);
        }
    }
}
