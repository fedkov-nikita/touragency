using Microsoft.AspNetCore.Mvc;
using Testovoe_zadaniye.AppServices.Interfaces;
using Testovoe_zadaniye.FileUploading;

namespace Testovoe_zadaniye.Controllers
{
    public class AdminController : Controller
    {
        readonly IReserveSave _resSave;
        public AdminController(IReserveSave resSave)
        {
            _resSave= resSave;
        }
        public IActionResult Reserver()
        {
            _resSave.ReserveMethod();
            return Ok();
        }
    }
}