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
    public class ToGuideController : Controller
    {
        TouragencyContext db;
        public ToGuideController(TouragencyContext context)
        {
            db = context;
        }
        public IActionResult ToGuide(int? id)
        {
            var viewModel = new TouristindexData();
            return View(db.Guides.Where(
                        i => i.GuideId == id.Value).ToList());
        }
    }
}
