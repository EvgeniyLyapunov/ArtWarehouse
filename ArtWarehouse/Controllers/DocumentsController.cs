using ArtWarehouse.Models.ModelsView;
using ArtWarehouse.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Rotativa.AspNetCore;
using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using PuppeteerSharp;

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

        [HttpGet]
        [Route("document-print-pdf")]
        public IActionResult CreatePdfFile(int id)
        {
            TempData["Enter"] = "Yes";

            SpecificDocument_MV documentGoods_MV = new SpecificDocument_MV();

            try
            {
                documentGoods_MV = _dbService.SpecificDocument_Get(id);
            }
            catch (Exception ex)
            {
                TempData["ErrorSoursPageMessage"] = "Ошибка получения данных из Базы Данных";
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Index", "Error");
            }

            var pdfContentTask = new ViewAsPdf("RequestFor_SpecificDocumet_Pdf", documentGoods_MV)
            {
                PageSize = Rotativa.AspNetCore.Options.Size.A4, // Формат страницы
                PageMargins = new Rotativa.AspNetCore.Options.Margins(0, 0, 0, 0), // Отступы в мм
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Landscape, // Горизонтальная ориентация
                CustomSwitches = "--keep-relative-links --enable-internal-links --keep-relative-links  --user-style-sheet ~/css/documents/document.css  "
            }.BuildFile(ControllerContext);

            try
            {
                pdfContentTask.Wait(); // Ждем завершения задачи
                var pdfContent = pdfContentTask.Result; // Получаем результат из задачи
                System.IO.File.WriteAllBytes("order-document.pdf", pdfContent);
            }
            catch (AggregateException ex)
            {
                foreach (var innerEx in ex.InnerExceptions)
                {
                    Debug.WriteLine($"Inner Exception: {innerEx.Message}");
                }
            }


            return RedirectToAction("Index", "Documents");
        }

    }
}
