using BusinessLayer.Abstract;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DynamicBlog.Areas.Admin.Views.ViewComponents.Statistic
{
    public class Statistic4 : ViewComponent
    {
        private readonly UserManager<AppUser> _userManager;

        public Statistic4(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            ViewBag.name = user.NameSurname;
            ViewBag.image = user.ImageUrl;
            ViewBag.description = user.About;
            ViewBag.mail = user.Email;
            return View();
        }
    }
}
