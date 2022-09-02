using BusinessLayer.Abstract;
using BusinessLayer.ValidationRules;
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
        private readonly IWriterService _writerService;
        private readonly WriterCity _writerCity;

        public RegisterController(IWriterService writerService, WriterCity writerCity)
        {
            _writerService = writerService;
            _writerCity = writerCity;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.Cities = _writerCity.GetCityList();
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Index(Writer writer, string passwordAgain, string cities, IFormFile imageFile)
        {
            AddProfileImage addProfileImage = new AddProfileImage();
            var validateWriter = _writerService.TGetByFilter(x => x.WriterMail == writer.WriterMail);
            if (ModelState.IsValid && writer.WriterPassword == passwordAgain && validateWriter == null)
            {
                writer.WriterStatus = true;
                writer.WriterAbout = "Deneme test";
                writer.WriterImage = AddProfileImage.ImageAdd(imageFile);
                _writerService.TAdd(writer);
                return RedirectToAction("Index", "Blog");
            }
            else if(writer.WriterPassword != passwordAgain)
            {
                ModelState.AddModelError("WriterPassword", "Girdiğiniz parolalar eşleşmedi lütfen tekrar deneyin.");
            }
            else if (validateWriter != null)
            {
                ModelState.AddModelError("ErrorMessage", "Girdiğiniz e-mail adresine sahip bir kullanıcı sistemde mevcut. Lütfen başka bir e-mail adresi giriniz.");
            }
            ViewBag.Cities = _writerCity.GetCityList();

            return View(writer);
        }
    }
}