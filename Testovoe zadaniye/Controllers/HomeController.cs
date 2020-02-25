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

namespace Testovoe_zadaniye.Controllers
{
    public class HomeController : Controller
    {
        TouragencyContext db;
        private readonly ILogger _logger;

        public HomeController(TouragencyContext context, ILoggerFactory logFactory)
        {
            _logger = logFactory.CreateLogger<HomeController>();
            db = context; 
        }
        public ActionResult Index()
        {
            _logger.LogInformation("Log message in the Index() method");
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
