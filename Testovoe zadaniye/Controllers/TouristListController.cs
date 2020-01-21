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
using Testovoe_zadaniye.Controllers;


namespace Testovoe_zadaniye.Controllers
{
    public class TouristListController : Controller
    {
        TouragencyContext db;
        public TouristListController(TouragencyContext context)
        {
            db = context;
        }
        public IActionResult TouristList()
        {
            return View(db.Tourists.ToList());
        }
    }
}
