using BusinessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DynamicBlog.Areas.Admin.Views.ViewComponents.Statistic
{
    public class Statistic4 : ViewComponent
    {
        private readonly IAdminService _adminService;

        public Statistic4(IAdminService adminService)
        {
            _adminService = adminService;
        }

        public IViewComponentResult Invoke()
        {
            var admin = _adminService.TGetByFilter(x => x.AdminId == 1);
            ViewBag.name = admin.Name;
            ViewBag.image = admin.ImageURL;
            ViewBag.description = admin.ShortDescription;
            return View();
        }
    }
}
