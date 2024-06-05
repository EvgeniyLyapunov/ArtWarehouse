using Microsoft.AspNetCore.Mvc;

namespace ArtWarehouse.Controllers
{
    [Route("[controller]")]
    public class InfoController : Controller
    {
        [HttpGet]
        [Route("info-help")]
        public IActionResult InfoHelp()
        {
            TempData["Enter"] = "Yes";
            return View();
        }
    }
}
