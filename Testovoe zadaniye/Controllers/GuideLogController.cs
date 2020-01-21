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
    public class GuideLogController : Controller
    {
        TouragencyContext db;
        public GuideLogController(TouragencyContext context)
        {
            db = context;
        }
        [HttpPost]
        public ActionResult GuideLog(GuideLogin guideLogin)
        {
            if (guideLogin.Name == null && guideLogin.Password == null) return RedirectToAction("Index");
            var g = db.Guides.Any(x => x.Login == guideLogin.Name && x.Password == guideLogin.Password);
            if (g == true)
            {
                return RedirectToAction("GuideSelection", "GuideSelection");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        [HttpGet]
        public ActionResult GuideLog()
        {
            return View();
        }
    }
}
