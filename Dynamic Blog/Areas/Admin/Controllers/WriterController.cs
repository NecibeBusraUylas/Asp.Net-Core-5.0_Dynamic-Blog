using DynamicBlog.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DynamicBlog.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class WriterController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult WriterList()
        {
            //listeyi bir değişkene atayıp onu json'a dönüştürme
            var JsonWriters = JsonConvert.SerializeObject(Writers);
            return Json(JsonWriters);
        }

        public IActionResult GetWriterById(int writerId)
        {
            var findWriter = Writers.FirstOrDefault(x => x.Id == writerId);
            var jsonWriters = JsonConvert.SerializeObject(findWriter);
            return Json(jsonWriters);
        }

        public static List<WriterClass> Writers = new List<WriterClass>
        {
            new WriterClass
            {
                Id=1,
                Name="Buşra"
            },
            new WriterClass
            {
                Id=2,
                Name="Mustafa"
            },
            new WriterClass
            {
                Id=3,
                Name="Ekin "
            }
        };

        [HttpPost]
        public IActionResult AddWriter(WriterClass writerClass)
        {
            Writers.Add(writerClass);
            var jsonWriters = JsonConvert.SerializeObject(writerClass);
            return Json(jsonWriters);
        }

        public IActionResult DeleteWriter(int id)
        {
            var writer = Writers.FirstOrDefault(x => x.Id == id);
            Writers.Remove(writer);
            return Json(writer);  // return Ok;
        }

        public IActionResult UpdateWriter(WriterClass writerClass)
        {
            var writer = Writers.FirstOrDefault(x => x.Id == writerClass.Id);
            writer.Name = writerClass.Name;
            var jsonWriters = JsonConvert.SerializeObject(writerClass);
            return Json(jsonWriters);
        }
    }
}