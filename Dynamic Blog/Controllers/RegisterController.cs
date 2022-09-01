using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.EntityFramework;
using DynamicBlog.Models;
using EntityLayer.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dynamic_Blog.Controllers
{
    public class RegisterController : Controller
    {
        WriterManager writerManager = new WriterManager(new EFWriterRepository());
        WriterCity writerCity = new WriterCity();

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.Cities = writerCity.GetCityList();
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Index(Writer writer, string passwordAgain, string cities, IFormFile imageFile)
        {
            WriterValidator wv = new WriterValidator();
            AddProfileImage addProfileImage = new AddProfileImage();
            ValidationResult results = wv.Validate(writer);
            var validateWriter = writerManager.TGetByFilter(x => x.WriterMail == writer.WriterMail);
            if (results.IsValid && writer.WriterPassword == passwordAgain && validateWriter == null)
            {
                writer.WriterStatus = true;
                writer.WriterAbout = "Deneme test";
                writer.WriterImage = AddProfileImage.ImageAdd(imageFile);
                writerManager.TAdd(writer);
                return RedirectToAction("Index", "Blog");
            }
            else if (!results.IsValid)
            {
                foreach (var item in results.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }
            else if(writer.WriterPassword != passwordAgain)
            {
                ModelState.AddModelError("WriterPassword", "Girdiğiniz parolalar eşleşmedi lütfen tekrar deneyin.");
            }
            else if (validateWriter != null)
            {
                ModelState.AddModelError("ErrorMessage", "Girdiğiniz e-mail adresine sahip bir kullanıcı sistemde mevcut. Lütfen başka bir e-mail adresi giriniz.");
            }
            ViewBag.Cities = writerCity.GetCityList();

            return View();
        }
    }
}