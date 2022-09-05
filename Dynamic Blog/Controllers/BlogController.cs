using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
        private CategoryManager _categoryManager = new CategoryManager(new EFCategoryRepository());
        private WriterManager _writerManager = new WriterManager(new EFWriterRepository());
        private BlogManager _blogManager = new BlogManager(new EFBlogRepository());
        private readonly UserManager<AppUser> _userManager;
        Context c = new Context();

        public BlogController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            var values = _blogManager.TGetBlogListWithCategory();
            return View(values);
        }

        [AllowAnonymous]
        public IActionResult BlogDetails(int id) 
        {
            ViewBag.i = id;
            var values = _blogManager.TGetBlogById(id);
            return View(values);
        }

        public async Task<IActionResult> BlogListByWriter() 
        {
            var userName = User.Identity.Name;
            var user = await _userManager.FindByNameAsync(userName);
            var values = _blogManager.TGetListWithCategoryByWriter(user.Id);
            return View(values);
        }

        [HttpGet]
        public IActionResult BlogAdd()
        {
            List<SelectListItem> categoryValues = (from x in _categoryManager.TGetList()
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
        public async Task<IActionResult> BlogAdd(Blog blog)
        {
            var userName = User.Identity.Name;
            var user = await _userManager.FindByNameAsync(userName);
            BlogValidator blogValidator = new BlogValidator();
            ValidationResult results = blogValidator.Validate(blog);
            if (results.IsValid)
            {
                var userMail = User.Identity.Name;
                blog.BlogStatus = true;
                blog.BlogCreationDate = DateTime.Parse(DateTime.Now.ToShortDateString());
                blog.WriterId = user.Id;
                _blogManager.TAdd(blog);
                return RedirectToAction("BlogListByWriter", "Blog");
            }
            else
            {
                foreach (var item in results.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }
            List<SelectListItem> categoryValues = (from x in _categoryManager.TGetList()
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
            var blogValue = _blogManager.TGetById(id);
            _blogManager.TDelete(blogValue);
            return RedirectToAction("BlogListByWriter");
        }

        public IActionResult ChangeStatusBlog(int id)
        {
            var blogValue = _blogManager.TGetById(id);
            if (blogValue.BlogStatus)
            {
                blogValue.BlogStatus = false;
            }
            else
            {
                blogValue.BlogStatus = true;
            }
            _blogManager.TUpdate(blogValue);
            return RedirectToAction("BlogListByWriter");
        }

        [HttpGet]
        public IActionResult BlogEdit(int id)
        {
            var blogValue = _blogManager.TGetById(id);
            List<SelectListItem> categoryValues = (from x in _categoryManager.TGetList()
                                                   select new SelectListItem
                                                   {
                                                       Text = x.CategoryName,
                                                       Value = x.CategoryId.ToString()
                                                   }).ToList();
            ViewBag.blogCategory = categoryValues;
            return View(blogValue);
        }

        [HttpPost]
        public async Task<IActionResult> BlogEdit(Blog blog)
        {
            BlogValidator blogValidator = new BlogValidator();
            ValidationResult results = blogValidator.Validate(blog);
            if (results.IsValid)
            {
                var userName = User.Identity.Name;
                var user = await _userManager.FindByNameAsync(userName);
                var value = _blogManager.TGetById(blog.BlogId);
                blog.WriterId = user.Id;
                blog.BlogId = value.BlogId;
                blog.BlogCreationDate = value.BlogCreationDate;
                blog.BlogStatus = true;
                _blogManager.TUpdate(blog);
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