using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Testovoe_zadaniye.DataBase;
using Testovoe_zadaniye.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Testovoe_zadaniye.AppServices.Interfaces;
using Testovoe_zadaniye.Models.OperationModels;

namespace Testovoe_zadaniye.Controllers
{
    public class GuideController : Controller
    {

        ILoggerCreator _loggerCreator;
        IGuideService _guideService;

        public GuideController(IGuideService guideService, ILoggerCreator loggerCreator)
        {
            _loggerCreator = loggerCreator;
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
        public async Task<IActionResult> AddGuide(NewGuideForm addGuide)
        {
            if (ModelState.IsValid)
            {
                var result = await _guideService.SaveNewGuideModel(addGuide);

                if (result == null)
                {
                    string message = "Save of new guide form";
                    var logger = _loggerCreator.FactoryMethod();
                    logger.LoggMessage(this.GetType().Name, message);
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
        public async Task<ActionResult> EditGuide(GuideEditForm model)
        {
            await _guideService.SaveCurrentEditedGuideForm(model);

            string message = "Edit of chosen Guide";
            var result = _loggerCreator.FactoryMethod();
            result.LoggMessage(this.GetType().Name, message);

            return Ok();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> GuideAuthentication(GuidesAuthenticationForm guideLogin)
        {
            if (ModelState.IsValid)
            {
                var result = await _guideService.ProccesingGuideAuthorizationForm(guideLogin);

                if (result != null)
                {
                    string message = "Successful Sign Up";
                    var logger = _loggerCreator.FactoryMethod();
                    logger.LoggMessage(this.GetType().Name, message);

                    await Authenticate(guideLogin.Name); // аутентификация

                    return RedirectToAction("GuideNavigation", "Guide");
                }

                string message2 = "Unsuccessful Sign Up";
                var logger1 = _loggerCreator.FactoryMethod();
                logger1.LoggMessage(this.GetType().Name, message2);

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
            return RedirectToAction("GuideAuthentication", "Guide");
        }

        [Authorize]
        public ActionResult GuideNavigation()
        {

            string message = "ShowGuideNavigationForm";
            var logger = _loggerCreator.FactoryMethod();
            logger.LoggMessage(this.GetType().Name, message);

            return View();
        }

        public async Task<IActionResult> GuideOfChosenTouristInfo(int? id)
        {
            string message = "Display tourist's guide";
            var logger = _loggerCreator.FactoryMethod();
            logger.LoggMessage(this.GetType().Name, message);

            var result = await _guideService.GuideInfoById(id);

            return View(result);
        }

        [Authorize]
        public async Task<ActionResult> AllGuidesList()
        {
            string message = "Display guides list";
            var logger = _loggerCreator.FactoryMethod();
            logger.LoggMessage(this.GetType().Name, message);

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
                var logger = _loggerCreator.FactoryMethod();
                logger.LoggMessage(this.GetType().Name, message);

                return RedirectToAction("AllGuidesList");
            }

            string message1 = "Deleting of Guide";
            var logger1 = _loggerCreator.FactoryMethod();
            logger1.LoggMessage(this.GetType().Name, message1);

            return NotFound();
        }

        [Authorize]
        public async Task<IActionResult> AllToursForGuide(int pageNumber = 1, int pageSize = 2)
        {
            
            string message = "Display tours List for guide";

            var logger = _loggerCreator.FactoryMethod();
            logger.LoggMessage(this.GetType().Name, message);

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