using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DynamicBlog.ViewComponents.Writer
{
    public class WriterNotification: ViewComponent
    {
        private NotificationManager notificationManager = new NotificationManager(new EFNotificationRepository());
        
        public IViewComponentResult Invoke()
        {
            var values = notificationManager.TGetList(x => x.NotificationStatus == true);
            if (values.Count() > 3)
            {
                values = values.TakeLast(3).ToList();
            }
            return View(values);
        }
    }
}