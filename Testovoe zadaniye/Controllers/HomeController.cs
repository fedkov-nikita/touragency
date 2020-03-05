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
using Microsoft.Extensions.Logging;
using Testovoe_zadaniye.LoggingMechanism;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Testovoe_zadaniye.Controllers
{
    public class HomeController : Controller
    {
        TouragencyContext db;
        readonly Logger logger;
        LoggerCreator combinedloggCreator;


        public HomeController(TouragencyContext context)
        {
            db = context;

            combinedloggCreator = new TextConsoleCreator();
            logger = combinedloggCreator.FactoryMethod();


        }
        public ActionResult Index()
        {
           
            string message = "Initial entering";
            string className = this.GetType().Name;

            logger.LoggMessage(className, message);

            return View();
        }

    }
}
