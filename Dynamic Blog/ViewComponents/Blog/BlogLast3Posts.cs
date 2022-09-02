using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DynamicBlog.ViewComponents.Blog
{
    public class BlogLast3Posts: ViewComponent
    {
        private readonly IBlogService _blogService;

        public BlogLast3Posts(IBlogService blogService)
        {
            _blogService = blogService;
        }

        public IViewComponentResult Invoke()
        {
            var values = _blogService.TGetLastBlogs(3);
            return View(values);
        }
    }
}