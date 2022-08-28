using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.EntityFramework;
using DynamicBlog.Models;
using EntityLayer.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DynamicBlog.Controllers
{
    //[Authorize]
    public class WriterController : Controller
    {
        WriterManager wm = new WriterManager(new EFWriterRepository());
        WriterCity writerCity = new WriterCity();

        //[Authorize]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult WriterProfile()
        {
            return View();
        }

        public IActionResult WriterMail()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult Test()
        {
            return View();
        }

        [AllowAnonymous]
        public PartialViewResult WriterNavbarPartial()
        {
            return PartialView();
        }

        [AllowAnonymous]
        public PartialViewResult WriterFooterPartial()
        {
            return PartialView();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult WriterEditProfile()
        {
            ViewBag.Cities = writerCity.GetCityList();
            var writerValues = wm.TGetById(1);
            return View(writerValues);
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult WriterEditProfile(Writer p, string passwordAgain, IFormFile imageFile)
        {
            WriterValidator validationRules = new WriterValidator();
            ValidationResult results = validationRules.Validate(p);
            if (results.IsValid)
            {
                wm.TUpdate(p);
                return RedirectToAction("Index", "Dashboard");
            }
            else
            {
                foreach (var item in results.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }
            return RedirectToAction("Index", "Dashboard");
        }
    }
}