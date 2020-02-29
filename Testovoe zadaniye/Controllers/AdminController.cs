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
using Testovoe_zadaniye.LoggingMechanism;
using Testovoe_zadaniye.FileUploading;
using Microsoft.AspNetCore.Hosting;

namespace Testovoe_zadaniye.Controllers
{
    public class AdminController : Controller
    {
        Builder builder;
        Director director;
        Product product;

        public AdminController()
        {
            director = new Director();
            builder = new ConcreteBuilder();
            director.Construct(builder);
            product = builder.GetResult();
        }
        public IActionResult Reserver()
        {
            product.ReserveMethod();
            return Ok();
        }
    }
}