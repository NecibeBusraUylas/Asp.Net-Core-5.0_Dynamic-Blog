using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DynamicBlog.ViewsComponents.Writer
{
    public class WriterNotification: ViewComponent
    {
        WriterManager wm = new WriterManager(new EFWriterRepository());
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
