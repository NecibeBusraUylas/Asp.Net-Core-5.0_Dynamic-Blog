using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DynamicBlog.Controllers
{
    public class AboutController : Controller
    {
        AboutManager aboutManager = new AboutManager(new EFAboutRepository());
        public IActionResult Index()
        {
            var Values = aboutManager.GetList();
            return View(Values);
        }
        public PartialViewResult SocialMediaAbout()
        {
            return PartialView();
        }
    }
}