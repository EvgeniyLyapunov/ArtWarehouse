using ArtWarehouse.Models.ModelsView;
using ArtWarehouse.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Linq;

namespace ArtWarehouse.Controllers
{
    [Route("[controller]")]
    [AllowAnonymous]
    public class PurchasingController : Controller
    {
        public IConfiguration Configuration { get; }

        Warehouse_DbService _warehouse_Db;
        Purchasing_DbService _purchasing_Db;

        public PurchasingController(IConfiguration configuration)
        {
            Configuration = configuration;
            _warehouse_Db = new Warehouse_DbService(Configuration);
            _purchasing_Db = new Purchasing_DbService(Configuration);
        }

        [HttpGet]
        [Route("purchasing-index")]

        public IActionResult Index()
        {
            TempData["Enter"] = "Yes";

            return View();
        }

        [HttpPost]
        [Route("purchasing-form")]

        public IActionResult PurchasingFormIndex(string data)
        {
            TempData["Enter"] = "Yes";

            var goodsIdList = JsonConvert.DeserializeObject<GoodsIdForPurchasingList_MV>(data);

            GoodsCompleteInfo_MV goodsCompleteInfo_MV = new GoodsCompleteInfo_MV();

            try
            {
                goodsCompleteInfo_MV = _warehouse_Db.GoodsWorkList_Get(goodsIdList.GoodsId);
            }
            catch (Exception ex)
            {
                TempData["ErrorSoursPageMessage"] = "Ошибка получения данных из Базы Данных";
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Index", "Error");
            }

            return View(goodsCompleteInfo_MV);
        }

        [HttpPost]
        [Route("purchasing-order")]
        public IActionResult PurchasingFormOrder(Purchasing_MV data)
        {
            TempData["Enter"] = "Yes";

            if (data.remaining_goods.Where(item => item == null).ToList().Count > 0 
                || data.remaining_goods.Where(item => item == "0").ToList().Count > 0)
            {
                TempData["ErrorSoursPageMessage"] = "Ошибка!";
                TempData["ErrorMessage"] = "Проверьте что бы небыло нулевых или пустых значений в заказе.";
                return RedirectToAction("Index", "Error");
            }

            try
            {
                _purchasing_Db.Purchasing_Insert(data);
            }
            catch (Exception ex)
            {
                TempData["ErrorSoursPageMessage"] = "Ошибка сохранения данных в Базу Данныx.";
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Index", "Error");
            }

            return RedirectToAction("Index", "Warehouse");
        }

    }
}
