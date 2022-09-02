using BusinessLayer.Abstract;
using BusinessLayer.ValidationRules;
using DynamicBlog.Models;
using EntityLayer.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dynamic_Blog.Controllers
{
    public class BlogController : Controller
    {
        private readonly IBlogService _blogService;
        private readonly ICategoryService _categoryService;
        private readonly IWriterService _writerService;
        private readonly GetUserInfo _userInfo;

        public BlogController(IBlogService blogService, ICategoryService categoryService, IWriterService writerService, GetUserInfo userInfo)
        {
            _blogService = blogService;
            _categoryService = categoryService;
            _writerService = writerService;
            _userInfo = userInfo;
        }

        public IActionResult Index()
        {
            var values = _blogService.TGetBlogListWithCategory();
            return View(values);
        }
        public IActionResult BlogDetails(int id) 
        {
            ViewBag.i = id;
            var values = _blogService.TGetBlogById(id);
            return View(values);
        }

        public IActionResult BlogListByWriter() 
        {
            int id = _userInfo.GetId(User);
            var values = _blogService.TGetListWithCategoryByWriter(id);
            return View(values);
        }

        [HttpGet]
        public IActionResult BlogAdd()
        {
            List<SelectListItem> categoryValues = (from x in _categoryService.TGetList()
                                                  select new SelectListItem
                                                  {
                                                      Text= x.CategoryName,
                                                      Value= x.CategoryId.ToString()
                                                      //Group= aynı ada sahip birden çok seçim yaptırmak için
                                                      //Disabled= seçime izin vermek alanın işlevini kapatır
                                                      //selected= ilkaçıldığında hangi value seçilmiş gelecek
                                                      
                                                  }).ToList();

            ViewBag.blogCategory = categoryValues;
            return View();
        }

        [HttpPost]
        public IActionResult BlogAdd(Blog blog)
        {
            BlogValidator blogValidator = new BlogValidator();
            ValidationResult results = blogValidator.Validate(blog);
            if (results.IsValid)
            {
                blog.BlogStatus = true;
                blog.BlogCreationDate = DateTime.Parse(DateTime.Now.ToShortDateString());
                blog.WriterId = _userInfo.GetId(User);
                _blogService.TAdd(blog);
                return RedirectToAction("BlogListByWriter", "Blog");
            }
            else
            {
                foreach (var item in results.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }
            List<SelectListItem> categoryValues = (from x in _categoryService.TGetList()
                                                   select new SelectListItem
                                                   {
                                                       Text = x.CategoryName,
                                                       Value = x.CategoryId.ToString()
                                                       //Group= aynı ada sahip birden çok seçim yaptırmak için
                                                       //Disabled= seçime izin vermek alanın işlevini kapatır
                                                       //selected= ilkaçıldığında hangi value seçilmiş gelecek

                                                   }).ToList();

            ViewBag.blogCategory = categoryValues;
            return View();
        }

        public IActionResult BlogDelete(int id)
        {
            var blogValue = _blogService.TGetById(id);
            _blogService.TDelete(blogValue);
            return RedirectToAction("BlogListByWriter");
        }

        public IActionResult ChangeStatusBlog(int id)
        {
            var blogValue = _blogService.TGetById(id);
            if (blogValue.BlogStatus)
            {
                blogValue.BlogStatus = false;
            }
            else
            {
                blogValue.BlogStatus = true;
            }
            _blogService.TUpdate(blogValue);
            return RedirectToAction("BlogListByWriter");
        }

        [HttpGet]
        public IActionResult BlogEdit(int id)
        {
            var blogValue = _blogService.TGetById(id);
            List<SelectListItem> categoryValues = (from x in _categoryService.TGetList()
                                                   select new SelectListItem
                                                   {
                                                       Text = x.CategoryName,
                                                       Value = x.CategoryId.ToString()
                                                   }).ToList();
            ViewBag.blogCategory = categoryValues;
            return View(blogValue);
        }

        [HttpPost]
        public IActionResult BlogEdit(Blog blog)
        {
            BlogValidator blogValidator = new BlogValidator();
            ValidationResult results = blogValidator.Validate(blog);
            if (results.IsValid)
            {
                var value = _blogService.TGetById(blog.BlogId);
                blog.WriterId = _userInfo.GetId(User);
                blog.BlogId = value.BlogId;
                blog.BlogCreationDate = value.BlogCreationDate;
                blog.BlogStatus = true;
                _blogService.TUpdate(blog);
            }
            else
            {
                foreach (var item in results.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }

            return RedirectToAction("BlogListByWriter");
        }
    }
}