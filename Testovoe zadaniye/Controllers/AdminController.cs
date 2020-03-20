using Microsoft.AspNetCore.Mvc;
using Testovoe_zadaniye.FileUploading;

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