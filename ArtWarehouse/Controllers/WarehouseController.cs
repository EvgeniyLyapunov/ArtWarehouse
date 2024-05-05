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
    public class WarehouseController : Controller
    {
        public IConfiguration Configuration { get; }

        Warehouse_DbService warehouse_Db;

        public WarehouseController(IConfiguration configuration)
        {
            warehouse_Db = new Warehouse_DbService(configuration);
            Configuration = configuration;
        }

        [HttpGet]
        [Route("warehouse-Index")]
        public IActionResult Index()
        {
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
        [Route("warehouse-Category")]
        public IActionResult CategorySort()
        {
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
    }
}
