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

namespace Testovoe_zadaniye.Controllers
{
    public class GuideLogController : Controller
    {
        TouragencyContext db;
        Logger logger;
        public GuideLogController(TouragencyContext context)
        {
            db = context;
            LoggerCreator loggerCreator = new TxtLoggerCreator();
            logger = loggerCreator.FactoryMethod();

        }
        [HttpPost]
        public ActionResult GuideLog(GuideLogin guideLogin)
        {
            if (guideLogin.Name == null && guideLogin.Password == null) return RedirectToAction("Index");
            var g = db.Guides.Any(x => x.Login == guideLogin.Name && x.Password == guideLogin.Password);
            if (g == true)
            {

                string message = "Successful Sign Up";
                string className = this.GetType().Name;

                logger.LoggMessage(className, message);

                return RedirectToAction("GuideSelection", "Navigation");
            }
            else
            {

                string message = "Unsuccessful Sign Up";
                string className = this.GetType().Name;

                logger.LoggMessage(className, message);

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
