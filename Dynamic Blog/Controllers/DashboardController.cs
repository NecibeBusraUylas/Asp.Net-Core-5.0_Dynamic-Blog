using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DynamicBlog.Controllers
{
    public class DashboardController : Controller
    {
        [AllowAnonymous]
        public IActionResult Index()
        {
            BlogManager blogManager = new BlogManager(new EFBlogRepository());
            CategoryManager categoryManager = new CategoryManager(new EFCategoryRepository());
            WriterManager writerManager = new WriterManager(new EFWriterRepository());
            string mail = ((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.Email).Value.ToString();
            int id = int.Parse(((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.Name).Value);
            ViewBag.toplamBlogSayisi = blogManager.TGetList(x => x.BlogStatus == true).Count();
            ViewBag.yazarinBlogSayisi = blogManager.GetBlogByWriter(id).Count();
            ViewBag.kategoriSayisi = categoryManager.TGetList().Count();
            return View();
        }
    }
}