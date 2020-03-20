using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Testovoe_zadaniye.DataBase;
using Testovoe_zadaniye.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Testovoe_zadaniye.LoggingMechanism;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Testovoe_zadaniye.AppServices.Interfaces;


namespace Testovoe_zadaniye.Controllers
{
    public class GuideController : Controller
    {
        TouragencyContext db;
        IWebHostEnvironment _appEnvironment;
        Logger logger;
        IGuideService _guideService;

        public GuideController(TouragencyContext context, IWebHostEnvironment appEnvironment, IGuideService guideService)
        {
            db = context;
            LoggerCreator loggerCreator = new TxtLoggerCreator();
            logger = loggerCreator.FactoryMethod();
            _appEnvironment = appEnvironment;
            _guideService = guideService;

        }
        [Authorize]
        public ActionResult AddGuide()
        {
            var result = _guideService.CreateNewGuideForm();
            return View(result);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddGuide(AddGuide addGuide)
        {
            if (ModelState.IsValid)
            {
                var result = await _guideService.SaveNewGuideModel(addGuide);

                if (result == null)
                {
                    return RedirectToAction("GuideNavigation", "Guide");
                }
                else
                    ModelState.AddModelError("Login", "Login is already exist");
            }
            return View(addGuide);
        }

        [Authorize]
        public async Task<ActionResult> EditGuide(int id = 0)
        {
            var result = await _guideService.CreateCurrentGuideEditForm(id);
            return View(result);
        }
        [Authorize]
        [HttpPost]
        public async Task<ActionResult> EditGuide(EditGuideForm model)
        {
            await _guideService.SaveCurrentEditedGuideForm(model);

            string message = "Edit of chosen Guide";
            logger.LoggMessage(this.GetType().Name, message);

            return Ok();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> GuideAuthentication(GuideLogin guideLogin)
        {
            if (ModelState.IsValid)
            {
                var result = await _guideService.ProccesingGuideAuthorizationForm(guideLogin);

                if (result != null)
                {
                    string message = "Successful Sign Up";
                    string className = this.GetType().Name;

                    logger.LoggMessage(className, message);
                    await Authenticate(guideLogin.Name); // аутентификация

                    return RedirectToAction("GuideNavigation", "Guide");
                }

                string message2 = "Unsuccessful Sign Up";
                string className2 = this.GetType().Name;

                logger.LoggMessage(className2, message2);

                ModelState.AddModelError(string.Empty, "Wrong login or password");

            }
            return View(guideLogin);
        }

        [HttpGet]
        public ActionResult GuideAuthentication()
        {
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("GuideLog", "GuideLog");
        }

        [Authorize]
        public ActionResult GuideNavigation()
        {

            string message = "Select of proper guide option";
            string className = this.GetType().Name;
            logger.LoggMessage(className, message);

            return View();
        }

        public async Task<IActionResult> GuideOfChosenTouristInfo(int? id)
        {

            string message = "Display tourist's guide";
            string className = this.GetType().Name;

            logger.LoggMessage(className, message);

            var result = await _guideService.GuideInfoById(id);

            return View(result);
        }

        [Authorize]
        public async Task<ActionResult> AllGuidesList()
        {

            string message = "Display guides list";
            string className = this.GetType().Name;

            logger.LoggMessage(className, message);

            var result = await _guideService.AllGuidesList();

            return View(result);
        }

        [HttpGet]
        [ActionName("DeleteGuide")]
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> DeleteGuide(int? id)
        {
            if (id != null)
            {
                await _guideService.DeleteCurrentGuide(id);

                string message = "Deleting of Guide";
                string className = this.GetType().Name;
                logger.LoggMessage(className, message);

                return RedirectToAction("AllGuidesList");
            }

            string message1 = "Deleting of Guide";
            string className1 = this.GetType().Name;
            logger.LoggMessage(className1, message1);

            return NotFound();
        }

        [Authorize]
        public async Task<IActionResult> AllToursForGuide(int pageNumber = 1, int pageSize = 2)
        {
            
            string message = "Display tours List for guide";
            string className = this.GetType().Name;

            logger.LoggMessage(className, message);

            var result = await _guideService.AllToursListForGuide(pageNumber, pageSize);

            return View(result);
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

    }
}