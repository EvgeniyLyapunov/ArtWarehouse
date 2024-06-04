using ArtWarehouse.Models.ModelsView;
using ArtWarehouse.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;

namespace ArtWarehouse.Controllers
{
    [Route("[controller]")]
    [AllowAnonymous]
    public class MovementController : Controller
    {
        public IConfiguration Configuration { get; }
        private Documents_DbService _dbService;

        public MovementController(IConfiguration configuration)
        {
            Configuration = configuration;
            _dbService = new Documents_DbService(Configuration);
        }

        public IActionResult Index()
        {
            Movement_MV model = new Movement_MV();

            try
            {
                model = _dbService.MovementGoods_Get();
            }
            catch (Exception ex)
            {
                TempData["ErrorSoursPageMessage"] = "Ошибка получения данных из Базы Данных";
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Index", "Error");
            }

            return View(model);
        }
    }
}
