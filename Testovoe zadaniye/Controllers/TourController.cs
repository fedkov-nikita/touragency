using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Testovoe_zadaniye.Models;
using Microsoft.AspNetCore.Authorization;
using Testovoe_zadaniye.AppServices.Interfaces;

namespace Testovoe_zadaniye.Controllers
{
    public class TourController : Controller
    {

        ILoggerCreator _loggerCreator;
        ITourService _tourService;

        public TourController(ITourService tourService, ILoggerCreator loggerCreator)
        {
            _loggerCreator = loggerCreator;
            _tourService = tourService;
        }

        [Authorize]
        public IActionResult AddTour()
        {
            var result =_tourService.CreateNewTourForm();
            return View(result);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddTour(AddTour addTour)
        {
            await _tourService.SaveNewTour(addTour);

            string message = "Add of new Tour";
            var logger = _loggerCreator.FactoryMethod();
            logger.LoggMessage(this.GetType().Name, message);

            return RedirectToAction("AllToursForGuide", "Guide");
        }

        [Authorize]
        public ActionResult EditTour(int id = 0)
        {
            var result = _tourService.CreateEditTourForm(id);
            return View(result);
        }

        [Authorize]
        [HttpPost]
        public ActionResult EditTour(EditTourForm model)
        {
            _tourService.SaveEditedTour(model);

            string message = "Edit of chosen Tour";
            var logger = _loggerCreator.FactoryMethod();
            logger.LoggMessage(this.GetType().Name, message);

            return Ok();
        }

        public async Task<IActionResult> ToursOfTourist(int? id)
        {
            string message = "Display tours";
            var logger = _loggerCreator.FactoryMethod();
            logger.LoggMessage(this.GetType().Name, message);

            //SQL useful realisation
            //var tours = db.Tours.FromSqlRaw("sp_ShowToursFromTT @TouristId={0}", id).ToList();
            //return View(tours);

            var tours = await _tourService.ToursOfChosenTourist(id);

            return View(tours);
        }

        [HttpGet]
        [ActionName("DeleteTour")]
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> DeleteTour(int? id)
        {
            if (id != null)
            {
                string message = "Deleting of Tour";
                var logger = _loggerCreator.FactoryMethod();
                logger.LoggMessage(this.GetType().Name, message);

                await _tourService.DeleteChosenTour(id);
                return RedirectToAction("AllToursForGuide", "Guide");
            }

            string message1 = "Unsucessful tour delete";
            var logger1 = _loggerCreator.FactoryMethod();
            logger1.LoggMessage(this.GetType().Name, message1);

            return NotFound();
        }

        public async Task<IActionResult> AllToursList()
        {

            string message = "Display tours List";
            var logger = _loggerCreator.FactoryMethod();
            logger.LoggMessage(this.GetType().Name, message);

            var result = await _tourService.ShowAllTours();

            return View(result);
        }
    }
}