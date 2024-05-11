using ArtWarehouse.Models.ModelsView;
using ArtWarehouse.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;

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

            GoodsCompleteForPrint_MV goodsComplete;

            try
            {
                GoodsCompleteInfo_MV goodsCompleteInfo_MV = _warehouse_Db.GoodsWorkList_Get(listForPrint.GoodsId);
                goodsComplete = new GoodsCompleteForPrint_MV(listForPrint.PrintViewType, goodsCompleteInfo_MV);
            }
            catch (Exception ex)
            {
                TempData["ErrorSoursPageMessage"] = "Ошибка получения данных из Базы Данных";
                TempData["ErrorMessage"] = ex.Message;
                return View();
            }



            return View(goodsComplete);
        }
    }
}
