using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Testovoe_zadaniye.DataBase;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authentication.Cookies;
using Testovoe_zadaniye.LoggingMechanism;
using Testovoe_zadaniye.Models;

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


            if (Request.Cookies.ContainsKey("name") 
                && Request.Cookies.ContainsKey($"password") 
                && Request.Cookies["name"] == guideLogin.Name 
                && Request.Cookies[$"password"] == guideLogin.Password) 
                return RedirectToAction("GuideSelection", "Navigation");
            

            var g = db.Guides.Any(x => x.Login == guideLogin.Name && x.Password == guideLogin.Password);
            if (g == true)
            {

                string message = "Successful Sign Up";
                string className = this.GetType().Name;

                logger.LoggMessage(className, message);

                if (Request.Cookies.ContainsKey("name"))
                {
                    string name = Request.Cookies["name"];
                    
                }
                else
                {
                    Response.Cookies.Append("name", "Tom");
                    
                }

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
