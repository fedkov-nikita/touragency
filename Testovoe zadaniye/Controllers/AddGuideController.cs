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
using Testovoe_zadaniye.Controllers;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

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
        [Authorize]
        public ActionResult ToGuideAdd()
        {
            AddGuide model = new AddGuide();

            return View(model);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> ToGuideAdd(AddGuide addGuide)
        {

            if (ModelState.IsValid)
            {
                Guide guide = await db.Guides.FirstOrDefaultAsync(u => u.Login == addGuide.Login);
                if (guide == null)
                {
                    // добавляем пользователя в бд
                    db.Guides.Add(new Guide { Name =addGuide.Name ,Login = addGuide.Login, Password = addGuide.Password });
                    await db.SaveChangesAsync();

                    await Authenticate(addGuide.Name); // аутентификация

                    return RedirectToAction("GuideSelection", "Navigation");
                }
                else
                    ModelState.AddModelError("", "Login is already exist");
            }
            return View(addGuide);

            //Guide guide = new Guide();
            //guide.Name = addGuide.Name;
            //guide.Login = addGuide.Login;
            //guide.Password = addGuide.Password;

            //if (guide.Name != null && guide.Login != null && guide.Password != null)
            //{
            //    db.Guides.Add(guide);
            //    db.SaveChanges();
            //}
            //else
            //{
            //    throw new Exception("Some field of guide is null");
            //}




            //string message = "Add of new Guide";
            //logger.LoggMessage(this.GetType().Name, message);

            //return RedirectToAction("GuideToToursAcces", "Navigation");
        }

        private async Task Authenticate(string userName)
        {
            // создаем один claim
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName)
            };
            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }
        [Authorize]
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
        [Authorize]
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