using BusinessLayer.Abstract;
using DataAccessLayer.Concrete;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
        private readonly IBlogService _blogService;
        private readonly ICategoryService _categoryService;
        private readonly UserManager<AppUser> _userManager;
        private Context c = new Context();

        public DashboardController(IBlogService blogService, ICategoryService categoryService, UserManager<AppUser> userManager)
        {
            _blogService = blogService;
            _categoryService = categoryService;
            _userManager = userManager;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            string userName = User.Identity.Name;
            //var userMail = c.Users.Where(x => x.UserName == userName).Select(y => y.Email).FirstOrDefault();
            //var writerId = c.Writers.Where(x=> x.WriterMail == userMail).Select(y=> y.WriterId).FirstOrDefault();


            var user = await _userManager.FindByNameAsync(userName);
            var id = user.Id;



            ViewBag.totalBlogCount = _blogService.TGetCount(x => x.BlogStatus == true);
            ViewBag.writerBlogCount = c.Blogs.Where(x => x.WriterId == id).Count();
            ViewBag.categoryCount = _categoryService.TGetCount(x => x.CategoryStatus == true);
            return View();
        }
    }
}