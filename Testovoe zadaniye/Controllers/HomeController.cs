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
        Logger logger;
        TextConsoleLogger texcon;

        public HomeController(TouragencyContext context)
        {
            db = context;
            LoggerCreator loggerCreator = new ConsoleLoggerCreator();
            logger = loggerCreator.FactoryMethod();

            TextConsoleLoggerCreator textConsoleLogger = new TextConsoleCreator();
            texcon = textConsoleLogger.FactoryMethod();
        }
        public ActionResult Index()
        {

            string message = "Initial entering";
            string className = this.GetType().Name;

            //logger.LoggMessage(className, message);
            texcon.CombinedLogMessage(className, message) ;


            return View();
        }

    }
}
