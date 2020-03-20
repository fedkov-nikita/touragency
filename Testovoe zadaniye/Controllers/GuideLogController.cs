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
        

    }
}
