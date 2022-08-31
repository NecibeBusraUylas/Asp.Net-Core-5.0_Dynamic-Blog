using BusinessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DynamicBlog.Areas.Admin.Views.ViewComponents.Statistic
{
    public class Statistic2 : ViewComponent
    {
        private readonly IBlogService _blogService;
        public Statistic2(IBlogService blogService)
        {
            _blogService = blogService;
        }
        public IViewComponentResult Invoke()
        {
            ViewBag.lastBlog = _blogService.TGetList().OrderByDescending(x => x.BlogId).Select(y => y.BlogTitle).Take(1).FirstOrDefault();
            return View();
        }
    }
}