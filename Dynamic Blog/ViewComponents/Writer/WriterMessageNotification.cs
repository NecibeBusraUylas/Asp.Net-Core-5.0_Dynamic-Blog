using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
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
        Message2Manager message2Manager = new Message2Manager(new EFMessage2Repository());
        public IViewComponentResult Invoke()
        {
            int id = int.Parse(((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.Name).Value);
            var values = message2Manager.TGetReceivingMessageListByWriter(id);
            if (values.Count() > 3)
            {
                values = values.TakeLast(3).ToList();
            }
            //ViewBag.NewMessage = values.Where(x => x.MessageStatus == true).Count();
            return View(values);
        }
    }
}