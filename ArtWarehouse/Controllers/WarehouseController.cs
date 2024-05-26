using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Dapper;
using System;
using ArtWarehouse.Models.ModelsView;
using ArtWarehouse.Models;
using ArtWarehouse.Models.ErrorModels;
using System.Linq;
using ArtWarehouse.Services;

namespace ArtWarehouse.Controllers
{
    [Route("[controller]")]
    public class WarehouseController : Controller
    {
        public IConfiguration Configuration { get; }

        Warehouse_DbService warehouse_Db;
        Sale_DbService sale_Db;

        public WarehouseController(IConfiguration configuration)
        {
            Configuration = configuration;
            warehouse_Db = new Warehouse_DbService(Configuration);
            sale_Db = new Sale_DbService(Configuration);
        }

        [HttpGet]
        [Route("warehouse-Index")]
        public IActionResult Index()
        {
            TempData["Enter"] = "Yes";

            GoodsCompleteInfo_MV goodsCompleteInfo_MV;

            try
            {
                if (AutoGenerateSales.FirstSaleAutoGenerate == false)
                {
                    AutoGenerateSales.SalesGenerator(warehouse_Db, sale_Db);
                    AutoGenerateSales.FirstSaleAutoGenerate = true;
                }

                goodsCompleteInfo_MV = warehouse_Db.GoodsCompleteInfo_Get();
            }
            catch (Exception ex)
            {
                TempData["ErrorSoursPageMessage"] = "Ошибка получения данных из Базы Данных";
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Index", "Error");
            }

            return View(goodsCompleteInfo_MV);
        }

        [HttpGet]
        [Route("warehouse-Category")]
        public IActionResult CategorySort()
        {
            TempData["Enter"] = "Yes";

            GoodsCompleteInfo_MV goodsCompleteInfo_MV;

            try
            {
                goodsCompleteInfo_MV = warehouse_Db.GoodsCompleteInfo_Get();
            }
            catch (Exception ex)
            {
                TempData["ErrorSoursPageMessage"] = "Ошибка получения данных из Базы Данных";
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Index", "Error");
            }

            return View(goodsCompleteInfo_MV);
        }

        [HttpGet]
        [Route("warehouse-Maker")]
        public IActionResult MakerSort()
        {
            TempData["Enter"] = "Yes";

            GoodsCompleteInfo_MV goodsCompleteInfo_MV;

            try
            {
                goodsCompleteInfo_MV = warehouse_Db.GoodsCompleteInfo_Get();
            }
            catch (Exception ex)
            {
                TempData["ErrorSoursPageMessage"] = "Ошибка получения данных из Базы Данных";
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Index", "Error");
            }

            return View(goodsCompleteInfo_MV);
        }

        [HttpGet]
        public IActionResult RequestFor_CardOfGoods(int id)
        {
            TempData["Enter"] = "Yes";

            GoodsInfoForCard_MV goodsInfoForCard_MV;

            try
            {
                goodsInfoForCard_MV = warehouse_Db.GoodsInfoForCard_Get(id);
            }
            catch (Exception ex)
            {
                TempData["ErrorSoursPageMessage"] = "Ошибка получения данных из Базы Данных";
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Index", "Error");
            }

            return View(goodsInfoForCard_MV);
        }
    }
}
