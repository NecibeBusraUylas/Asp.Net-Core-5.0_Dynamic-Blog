using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DynamicBlog.ViewsComponents.Writer
{
    public class WriterAboutOnDashboard: ViewComponent
    {
        WriterManager writerManager = new WriterManager(new EFWriterRepository());
        Context c = new Context();
        public IViewComponentResult Invoke()
        {
            //string mail = User.Identity.Name;
            //var writerId = c.Writers.Where(x => x.WriterMail == mail).Select(y => y.WriterId).FirstOrDefault();
            //var values = writerManager.GetWriterById(writerId);
            //return View(values);
            var values = writerManager.TGetByFilter(x => x.WriterId == int.Parse(User.Identity.Name));
            return View(values);
        }
    }
}