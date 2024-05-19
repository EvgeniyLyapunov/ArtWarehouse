using ArtWarehouse.Models.ModelsView;
using ArtWarehouse.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;

namespace ArtWarehouse.Controllers
{
    [Route("[controller]")]
    public class DocumentsController : Controller
    {
        public IConfiguration Configuration { get; }
        private Documents_DbService _dbService;

        public DocumentsController(IConfiguration configuration)
        {
            Configuration = configuration;
            _dbService = new Documents_DbService(Configuration);
        }


        public IActionResult Index()
        {
            TempData["Enter"] = "Yes";
            DocumentsList_MV documentsList_MV = new DocumentsList_MV();

            try
            {
                documentsList_MV = _dbService.DocumentsList_Get();
            }
            catch (Exception ex)
            {
                TempData["ErrorSoursPageMessage"] = "Ошибка получения данных из Базы Данных";
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Index", "Error");
            }

            return View(documentsList_MV);
        }
    }
}
