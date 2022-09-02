using BusinessLayer.Abstract;
using BusinessLayer.ValidationRules;
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
        private readonly IWriterService _writerService;
        private readonly GetUserInfo _userInfo;
        private readonly WriterCity _writerCity;

        public WriterController(IWriterService writerService, GetUserInfo userInfo, WriterCity writerCity)
        {
            _writerService = writerService;
            _userInfo = userInfo;
            _writerCity = writerCity;
        }

        public IActionResult Index()
        {
            string mail = ((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.Email).Value.ToString();
            string id = ((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.Name).Value;
            ViewBag.id = id;
            ViewBag.userMail = mail;
            ViewBag.Name = _writerService.TGetByFilter(x => x.WriterMail == mail).WriterName;
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
            ViewBag.Cities = _writerCity.GetCityList();
            var writerValues = _writerService.TGetByFilter(x => x.WriterId == _userInfo.GetId(User));
            return View(writerValues);
        }

        [HttpPost]
        public IActionResult WriterEditProfile(Writer writer, string passwordAgain, IFormFile imageFile)
        {
            WriterValidator validationRules = new WriterValidator();
            var values = _writerService.TGetById(_userInfo.GetId(User));
            if (writer.WriterPassword == null)
            {
                writer.WriterPassword = values.WriterPassword;
                passwordAgain = writer.WriterPassword;
            }
            ValidationResult results = validationRules.Validate(writer);
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
                    writer.WriterImage = AddProfileImage.ImageAdd(imageFile);
                }
                writer.WriterStatus = values.WriterStatus;
                _writerService.TUpdate(writer);
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
            ViewBag.Cities = _writerCity.GetCityList();
            return View();
        }
    }
}