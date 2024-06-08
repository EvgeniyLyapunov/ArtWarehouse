using ArtWarehouse.Models;
using ArtWarehouse.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ArtWarehouse.Controllers
{
    public class HomeController : Controller
    {
        public IConfiguration Configuration { get; }
        Users_DbService users_Db;
        public HomeController(IConfiguration configuration)
        {
            Configuration = configuration;
            users_Db = new Users_DbService(Configuration);
        }

        // страница с названием проекта и кнопкой вход
        public IActionResult Index()
        {
            TempData["Enter"] = "No";
            return View();
        }

        // страница с формой ввода никнейма и пароля
        public IActionResult Enter()
        {
            TempData["Enter"] = "No";
            return View();
        }

        // проверка пароля и редирект на соответствующие страницы
        [HttpPost]
        [Route("/user-enter")]
        public IActionResult EnterSystem([FromForm] UserEnter_Model userEnter) 
        {
            var hash = GetMD5Hach.CreateMD5(userEnter.Password);
            UserEnter_Model model = new UserEnter_Model
            {
                Nickname = userEnter.Nickname,
                Password = hash
            };

            try
            {
                var user = users_Db.EnterUser(model);

                if (user.Name != "Error")
                {
                    return RedirectToAction("Index", "Warehouse", new { username = user.Name });
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorSoursPageMessage"] = "Ошибка получения данных из Базы Данных";
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
