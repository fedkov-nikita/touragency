using Microsoft.AspNetCore.Mvc;
using Testovoe_zadaniye.AppServices.Interfaces;

namespace Testovoe_zadaniye.Controllers
{
    public class HomeController : Controller
    {
        ILoggerCreator _loggerCreator;

        public HomeController(ILoggerCreator loggerCreator)
        {
            _loggerCreator = loggerCreator;
        }
        public ActionResult Index()
        {
           
            string message = "Initial entering";
            string className = this.GetType().Name;
            var result = _loggerCreator.FactoryMethod();
            result.LoggMessage(className, message);

            return View();
        }
    }
}
