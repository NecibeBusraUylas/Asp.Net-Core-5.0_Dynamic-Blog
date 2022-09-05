using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DynamicBlog.ViewComponents.Writer
{
    public class WriterAboutOnDashboard: ViewComponent
    {
        private WriterManager writerManager = new WriterManager(new EFWriterRepository());
        private readonly UserManager<AppUser> _userManager;
        private Context c = new Context();

        public WriterAboutOnDashboard(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync() 
        { 
            string userName = User.Identity.Name;
            var user = await _userManager.FindByNameAsync(userName);
            return View(user);
        }
    }
}