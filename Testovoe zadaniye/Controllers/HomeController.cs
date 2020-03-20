using Microsoft.AspNetCore.Mvc;
using Testovoe_zadaniye.DataBase;
using Testovoe_zadaniye.LoggingMechanism;

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
