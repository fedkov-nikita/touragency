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
using Microsoft.AspNetCore.Http;
using System.Runtime.CompilerServices;
using Testovoe_zadaniye.LoggingMechanism;
using Testovoe_zadaniye.FileUploading;


namespace Testovoe_zadaniye.Controllers
{
    public class AddGuideController : Controller
    {
        TouragencyContext db;
        IWebHostEnvironment _appEnvironment;
        Logger logger;

        public AddGuideController(TouragencyContext context, IWebHostEnvironment appEnvironment)
        {
            db = context;
            LoggerCreator loggerCreator = new TxtLoggerCreator();
            logger = loggerCreator.FactoryMethod();
            _appEnvironment = appEnvironment;

        }
        public ActionResult ToGuideAdd()
        {
            AddGuide model = new AddGuide();

            return View(model);
        }

        [HttpPost]
        public IActionResult ToGuideAdd(AddGuide addGuide)
        {

            Guide guide = new Guide();
            guide.Name = addGuide.Name;
            guide.Login = addGuide.Login;
            guide.Password = addGuide.Password;

            if (guide.Name != null && guide.Login != null && guide.Password != null)
            {
                db.Guides.Add(guide);
                db.SaveChanges();
            }
            else
            {
                throw new Exception("Some field of guide is null");
            }


            

            string message = "Add of new Guide";
            logger.LoggMessage(this.GetType().Name, message);

            return RedirectToAction("GuideToToursAcces", "Navigation");
        }

        public ActionResult ToGuideEdit(int id = 0)
        {
            Guide guide = new Guide();
            if (id != 0)
            {
                guide = db.Guides.Where(x => x.GuideId == id).FirstOrDefault();
            }
            EditGuideForm model = new EditGuideForm();
            model.Name = guide.Name;
            model.Login = guide.Login;
            model.Password = guide.Password;
            model.GuideId = guide.GuideId;
            


            return View(model);
        }

        [HttpPost]
        public ActionResult ToGuideEdit(EditGuideForm model)
        {
            Guide guide = new Guide();
            guide.Name = model.Name;
            guide.Login = model.Login;
            guide.Password = model.Password;
            guide.GuideId = model.GuideId;

            if (model.GuideId != 0)
            {
                db.Entry(guide).State = EntityState.Modified;
                db.SaveChanges();
            }
            string message = "Edit of chosen Guide";
            logger.LoggMessage(this.GetType().Name, message);

            return Ok();
        }
    }
}