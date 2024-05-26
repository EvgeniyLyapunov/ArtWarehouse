using ArtWarehouse.Models.ModelsView;
using ArtWarehouse.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;

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

        [HttpGet]
        [Route("Documents-Index")]
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

        [HttpGet]
        [Route("Documents-ExpenseSort")]
        public IActionResult DocumentsExpenseSort()
        {
            TempData["Enter"] = "Yes";
            DocumentsList_MV documentsList_MV = new DocumentsList_MV();

            try
            {
                documentsList_MV = _dbService.DocumentsList_Get();
                documentsList_MV.Documents = documentsList_MV.Documents.Where(o => o.Type_Order == "sale of goods").ToList();
            }
            catch (Exception ex)
            {
                TempData["ErrorSoursPageMessage"] = "Ошибка получения данных из Базы Данных";
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Index", "Error");
            }

            return View(documentsList_MV);
        }

        [HttpGet]
        [Route("Documents-ReceiptSort")]
        public IActionResult DocumentsReceiptSort()
        {
            TempData["Enter"] = "Yes";
            DocumentsList_MV documentsList_MV = new DocumentsList_MV();

            try
            {
                documentsList_MV = _dbService.DocumentsList_Get();
                documentsList_MV.Documents = documentsList_MV.Documents.Where(o => o.Type_Order == "arrival of goods").ToList();
            }
            catch (Exception ex)
            {
                TempData["ErrorSoursPageMessage"] = "Ошибка получения данных из Базы Данных";
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Index", "Error");
            }

            return View(documentsList_MV);
        }

        [HttpGet]
        [Route("Documents-SpecificDocumet")]

        public IActionResult RequestFor_SpecificDocumet(int id)
        {
            TempData["Enter"] = "Yes";
            SpecificDocument_MV documentGoods_MV = new SpecificDocument_MV();

            try
            {
                documentGoods_MV = _dbService.SpecificDocument_Get(id);
            }
            catch(Exception ex)
            {
                TempData["ErrorSoursPageMessage"] = "Ошибка получения данных из Базы Данных";
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Index", "Error");
            }

            return View(documentGoods_MV);
        }
    }
}
