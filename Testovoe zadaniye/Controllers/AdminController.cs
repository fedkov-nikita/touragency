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
        ReserveSave resSave;
        public AdminController()
        {
            resSave = new ReserveSave();
        }
        public IActionResult Reserver()
        {
            resSave.ReserveMethod();
            return Ok();
        }
    }
}