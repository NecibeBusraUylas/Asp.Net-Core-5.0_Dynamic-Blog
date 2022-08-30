using ClosedXML.Excel;
using DataAccessLayer.Concrete;
using DynamicBlog.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DynamicBlog.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BlogController : Controller
    {
        // ExportStaticBlogList'i verilerini GetBlogList()'tan alarak tetikleyecek.
        public IActionResult BlogListExcel()
        {
            return View();
        }

        public IActionResult ExportStaticBlogList()
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Blog Listesi");
                worksheet.Cell(1, 1).Value = "Blog Id";
                worksheet.Cell(1, 2).Value = "Blog Adı";

                int BlogRowCount = 2; //1.satır başlık o yüzden 2. satırdan başlayacak.
                foreach(var item in GetBlogList())
                {
                    worksheet.Cell(BlogRowCount, 1).Value = item.Id;
                    worksheet.Cell(BlogRowCount, 2).Value = item.BlogName;
                    BlogRowCount++;
                }
                using(var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    // dosya dön bu dosyanın içeriği şu olsun excel döküman formatı, dosya ismi bu olacak
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "BlogListesi1.xlsx");
                }
            }
                //return View();
        }

        public List<BlogModel> GetBlogList()
        {
            List<BlogModel> blogModel = new List<BlogModel>
            {
                new BlogModel{Id = 1, BlogName = "C# Programlamaya Giriş"},
                new BlogModel{Id = 2, BlogName = "Tesla Firmasının Araçları"},
                new BlogModel{Id = 3, BlogName = "2020 Olimpiyatları"},
            };
            return blogModel;
        }

        // ExportDynamicBlogList'i verilerini GetBlogListDB()'DEn alarak tetikleyecek.
        public IActionResult BlogListExcelDB()
        {
            return View();
        }

        public IActionResult ExportDynamicBlogList()
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Blog Listesi");
                worksheet.Cell(1, 1).Value = "Blog ID";
                worksheet.Cell(1, 2).Value = "Blog Adı";

                int BloRowCount = 2;
                foreach (var item in GetBlogListDB())
                {
                    worksheet.Cell(BloRowCount, 1).Value = item.Id;
                    worksheet.Cell(BloRowCount, 2).Value = item.BlogName;
                    BloRowCount++;
                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet","BlogListesiDB.xlsx");
                }
            }
        }
        public List<BlogModelDB> GetBlogListDB()
        {
            List<BlogModelDB> blogModelDB = new List<BlogModelDB>();
            using (var c = new Context())
            {
                blogModelDB = c.Blogs.Select(x => new BlogModelDB
                {
                    Id = x.BlogId,
                    BlogName = x.BlogTitle
                }).ToList();
                return blogModelDB;
            }
        }
    }
}