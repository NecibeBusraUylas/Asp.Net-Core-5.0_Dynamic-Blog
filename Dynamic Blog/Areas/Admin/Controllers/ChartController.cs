using DynamicBlog.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DynamicBlog.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ChartController : Controller
    {
        // kategorilerin grafik üzerinde listeleneceği action
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CategoryChart()
        {
            List<CategoryClass> list = new List<CategoryClass>();
            list.Add(new CategoryClass
            {
                categoryName = "Teknoloji",
                categoryCount = 8
            });
            list.Add(new CategoryClass
            {
                categoryName = "Yazılım",
                categoryCount = 5
            });
            list.Add(new CategoryClass
            {
                categoryName = "Spor",
                categoryCount = 2
            });
            list.Add(new CategoryClass
            {
                categoryName = "Sinema",
                categoryCount = 6
            });
            return Json(new { jsonlist = list }); // chartları json formatında bir script ile çağıracağım
        }
    }
}