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
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;

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
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> GuideLog(GuideLogin guideLogin)
        {
            if (ModelState.IsValid)
            {
                Guide guide = await db.Guides.FirstOrDefaultAsync(c => c.Login == guideLogin.Name && c.Password == guideLogin.Password);
                if (guide != null)
                {
                    string message = "Successful Sign Up";
                    string className = this.GetType().Name;

                    logger.LoggMessage(className, message);
                    await Authenticate(guideLogin.Name); // аутентификация

                    return RedirectToAction("GuideSelection", "Navigation");
                }
                ModelState.AddModelError("", "Wrong login or password");

            }
            return View(guideLogin);
        }

        [HttpGet]
        public ActionResult GuideLog()
        {
            return View();
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

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("GuideLog", "GuideLog");
        }

    }
}
