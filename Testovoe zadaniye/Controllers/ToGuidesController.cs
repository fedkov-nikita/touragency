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
    public class ToGuidesController : Controller
    {
        TouragencyContext db;
        public ToGuidesController(TouragencyContext context)
        {
            db = context;
        }
        public ActionResult ToGuides()
        {
            return View(db.Guides.ToList());
        }

    }
}
