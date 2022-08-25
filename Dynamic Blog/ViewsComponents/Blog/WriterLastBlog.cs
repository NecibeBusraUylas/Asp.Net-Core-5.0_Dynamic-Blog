using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DynamicBlog.ViewsComponents.Blog
{
    public class WriterLastBlog: ViewComponent
    {
        BlogManager bm = new BlogManager(new EFBlogRepository());
        public IViewComponentResult Invoke()
        {
            var values = bm.GetBlogByWriter(1);
            return View(values);
        }
    }
}