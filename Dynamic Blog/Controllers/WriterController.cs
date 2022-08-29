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
using System.Security.Claims;
using System.Threading.Tasks;

namespace DynamicBlog.Controllers
{
    public class WriterController : Controller
    {
        WriterManager writerManager = new WriterManager(new EFWriterRepository());
        WriterCity writerCity = new WriterCity();

        public IActionResult Index()
        {
            string mail = ((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.Email).Value.ToString();
            string id = ((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.Name).Value;
            ViewBag.id = id;
            ViewBag.userMail = mail;
            ViewBag.Name = writerManager.TGetByFilter(x => x.WriterMail == mail).WriterName;
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

        public IActionResult Test()
        {
            return View();
        }

        public PartialViewResult WriterNavbarPartial()
        {
            return PartialView();
        }

        public PartialViewResult WriterFooterPartial()
        {
            return PartialView();
        }

        [HttpGet]
        public IActionResult WriterEditProfile()
        {
            ViewBag.Cities = writerCity.GetCityList();
            var writerValues = writerManager.TGetByFilter(x => x.WriterId == int.Parse(User.Identity.Name));
            return View(writerValues);
        }

        [HttpPost]
        public IActionResult WriterEditProfile(Writer writer, string passwordAgain, IFormFile imageFile)
        {
            var imgLocation = "";
            WriterValidator validationRules = new WriterValidator();
            AddProfileImage addProfileImage = new AddProfileImage();
            ValidationResult results = validationRules.Validate(writer);
            var values = writerManager.TGetById(int.Parse(User.Identity.Name));
            if (results.IsValid && writer.WriterPassword == passwordAgain)
            {
                if (writer.WriterPassword == null)
                {
                    writer.WriterPassword = values.WriterPassword;
                    passwordAgain = writer.WriterPassword;
                }
                else if (imageFile == null)
                {
                    writer.WriterImage = values.WriterImage;
                }
                else if(imageFile != null)
                {
                    imgLocation = addProfileImage.ImageAdd(imageFile, out string imageName);
                    writer.WriterImage = imgLocation;
                }
                writer.WriterStatus = values.WriterStatus;
                writerManager.TUpdate(writer);
                return RedirectToAction("Index", "Dashboard");
            }
            else if (!results.IsValid)
            {
                foreach (var item in results.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }
            else if (writer.WriterPassword != passwordAgain && writer.WriterPassword != values.WriterPassword)
            {
                ModelState.AddModelError("PasswordAgainMessage","Girdiğiniz parolalar eşleşmedi lütfen tekrar deneyin.");
            }
            ViewBag.Cities = writerCity.GetCityList();
            return View();
        }
    }
}