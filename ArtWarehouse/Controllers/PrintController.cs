using ArtWarehouse.Models;
using ArtWarehouse.Models.ModelsView;
using ArtWarehouse.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Rotativa.AspNetCore;
using System.Diagnostics;

namespace ArtWarehouse.Controllers
{
    [Route("[controller]")]
    [AllowAnonymous]
    public class PrintController : Controller
    {
        public IConfiguration Configuration { get; }

        Warehouse_DbService _warehouse_Db;

        public PrintController(IConfiguration configuration)
        {
            Configuration = configuration;
            _warehouse_Db = new Warehouse_DbService(Configuration);
        }

        [HttpPost]
        [Route("print-index")]
        public IActionResult Index(string data)
        {
            var listForPrint = JsonConvert.DeserializeObject<ListForPrint_MV>(data);

            GoodsCompleteForPrint_MV goodsComplete = new GoodsCompleteForPrint_MV();
            GoodsCompleteInfo_MV goodsCompleteInfo_MV;
            try
            {
                if (listForPrint.GoodsId.Length == 0)
                {
                    goodsCompleteInfo_MV = _warehouse_Db.GoodsCompleteInfo_Get();
                }
                else
                {
                    goodsCompleteInfo_MV = _warehouse_Db.GoodsWorkList_Get(listForPrint.GoodsId);
                }
                goodsComplete.GoodsList = (List<Goods_Model>)goodsCompleteInfo_MV.goodsList;
                goodsComplete.CategoriesList = (List<GoodsCategory_Model>)goodsCompleteInfo_MV.categoriesList;
                goodsComplete.MakersList = (List<Maker_Model>)goodsCompleteInfo_MV.makersList;
                goodsComplete.PrintViewType = listForPrint.PrintViewType;
            }
            catch (Exception ex)
            {
                TempData["ErrorSoursPageMessage"] = "Ошибка получения данных из Базы Данных";
                TempData["ErrorMessage"] = ex.Message;
                return View();
            }

            return View(goodsComplete);
        }

        [HttpPost]
        [Route("print-pdf")]
        public IActionResult CreatePdfFile(GoodsCompleteForPrint_MV goodsComplete) 
        {
            GoodsCompleteForPrint_MV model = goodsComplete;

            var pdfContentTask = new ViewAsPdf("PrintPage", model)
            {
                PageSize = Rotativa.AspNetCore.Options.Size.A4, // Формат страницы
                PageMargins = new Rotativa.AspNetCore.Options.Margins(0, 0, 0, 0), // Отступы в мм
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait, // Горизонтальная ориентация
            }.BuildFile(ControllerContext);

            try
            {
                pdfContentTask.Wait(); // Ждем завершения задачи
                var pdfContent = pdfContentTask.Result; // Получаем результат из задачи
                System.IO.File.WriteAllBytes("document.pdf", pdfContent);
            }
            catch (AggregateException ex)
            {
                foreach (var innerEx in ex.InnerExceptions)
                {
                    Debug.WriteLine($"Inner Exception: {innerEx.Message}");
                }
            }


            return RedirectToAction("Index", "Warehouse");
        }


    }
}
