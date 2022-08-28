using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DynamicBlog.Controllers
{
    public class DashboardController : Controller
    {
        [AllowAnonymous]
        public IActionResult Index()
        {
            BlogManager bm = new BlogManager(new EFBlogRepository());
            CategoryManager cm = new CategoryManager(new EFCategoryRepository());
            ViewBag.toplamBlogSayisi = bm.TGetList(x => x.BlogStatus == true).Count();
            ViewBag.yazarinBlogSayisi = bm.GetBlogByWriter(1).Count();
            ViewBag.kategoriSayisi = cm.TGetList(null).Count();
            return View();
        }
    }
}