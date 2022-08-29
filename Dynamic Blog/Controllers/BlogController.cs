using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.EntityFramework;
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
        BlogManager blogManager = new BlogManager(new EFBlogRepository());
        WriterManager writerManager = new WriterManager(new EFWriterRepository());
        CategoryManager categoryManager = new CategoryManager(new EFCategoryRepository());
        public IActionResult Index()
        {
            var values = blogManager.GetBlogListWithCategory();
            return View(values);
        }
        public IActionResult BlogDetails(int id) 
        {
            ViewBag.i = id;
            var values = blogManager.GetBlogById(id);
            return View(values);
        }

        public IActionResult BlogListByWriter() 
        {
            int id = writerManager.TGetByFilter(x => x.WriterMail == User.Identity.Name).WriterId;
            var values = blogManager.GetListWithCategoryByWriter(id);
            return View(values);
        }

        [HttpGet]
        public IActionResult BlogAdd()
        {
            List<SelectListItem> categoryValues = (from x in categoryManager.TGetList()
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
                blog.WriterId = writerManager.TGetByFilter(x => x.WriterMail == User.Identity.Name).WriterId;
                blogManager.TAdd(blog);
                return RedirectToAction("BlogListByWriter", "Blog");
            }
            else
            {
                foreach (var item in results.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }
            List<SelectListItem> categoryValues = (from x in categoryManager.TGetList()
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
            var blogValue = blogManager.TGetById(id);
            blogManager.TDelete(blogValue);
            return RedirectToAction("BlogListByWriter");
        }

        public IActionResult ChangeStatusBlog(int id)
        {
            var blogValue = blogManager.TGetById(id);
            if (blogValue.BlogStatus)
            {
                blogValue.BlogStatus = false;
            }
            else
            {
                blogValue.BlogStatus = true;
            }
            blogManager.TUpdate(blogValue);
            return RedirectToAction("BlogListByWriter");
        }

        [HttpGet]
        public IActionResult BlogEdit(int id)
        {
            var blogValue = blogManager.TGetById(id);
            List<SelectListItem> categoryValues = (from x in categoryManager.TGetList()
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
                var value = blogManager.TGetById(blog.BlogId);
                blog.WriterId = writerManager.TGetByFilter(x => x.WriterMail == User.Identity.Name).WriterId;
                blog.BlogId = value.BlogId;
                blog.BlogCreationDate = value.BlogCreationDate;
                blog.BlogStatus = true;
                blogManager.TUpdate(blog);
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