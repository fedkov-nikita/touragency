﻿using System;
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
using Microsoft.Extensions.Logging;
using Testovoe_zadaniye.LoggingMechanism;

namespace Testovoe_zadaniye.Controllers
{
    public class HomeController : Controller
    {
        TouragencyContext db;
        //Logging logging = Logging.getInstance();

        public HomeController(TouragencyContext context)
        {
            db = context; 

        }
        public ActionResult Index()
        {

            //string message = "Initial entering";
            //string className = this.GetType().Name;

            //logging.LoggMessage(className, message);

            LoggerCreator loggerCreator = new TxtLoggerCreator();
            loggerCreator.FactoryMethod();


            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
