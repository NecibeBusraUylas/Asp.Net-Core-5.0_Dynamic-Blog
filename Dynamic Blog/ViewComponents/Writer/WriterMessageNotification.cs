using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DynamicBlog.ViewComponents.Writer
{
    public class WriterMessageNotification : ViewComponent
    {
        private Message2Manager message2Manager = new Message2Manager(new EFMessage2Repository());
        private Context c = new Context();
        private readonly UserManager<AppUser> _userManager;

        public WriterMessageNotification(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var userName = User.Identity.Name;
            var user = await _userManager.FindByNameAsync(userName);
            var values = message2Manager.TGetReceivingMessageListByWriter(user.Id);
            if (values.Count() > 3)
            {
                values = values.TakeLast(3).ToList();
            }
            return View(values);
        }
    }
}