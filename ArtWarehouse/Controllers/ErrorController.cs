using ArtWarehouse.Models.ErrorModels;
using Microsoft.AspNetCore.Mvc;

namespace ArtWarehouse.Controllers
{
    public class ErrorController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
