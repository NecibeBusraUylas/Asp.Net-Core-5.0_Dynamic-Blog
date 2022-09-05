using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using DynamicBlog.Models;
using EntityLayer.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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
        private readonly WriterCity _writerCity;
        private readonly UserManager<AppUser> _userManager;
        private Context c = new Context();

        public WriterController(IWriterService writerService, WriterCity writerCity, UserManager<AppUser> userManager)
        {
            _writerService = writerService;
            _writerCity = writerCity;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var userName = User.Identity.Name;
            var user = await _userManager.FindByNameAsync(userName);
            ViewBag.name = user.NameSurname;
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
        public async Task<IActionResult> WriterEditProfile()
        {
            //ViewBag.Cities = _writerCity.GetCityList();
            //var writerValues = _writerService.TGetByFilter(x => x.WriterId == _userInfo.GetId(User));
            //return View(writerValues);
            var values = await _userManager.FindByNameAsync(User.Identity.Name);
            UserUpdateViewModel model = new UserUpdateViewModel();
            model.Mail = values.Email;
            model.NameSurname = values.NameSurname;
            model.Username = values.UserName;
            model.ImageUrl = values.ImageUrl;
            model.Username = values.UserName;
            model.About = values.About;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> WriterEditProfile(UserUpdateViewModel userUpdateViewModel) //IFormFile imageFile
        {
            var values = await _userManager.FindByNameAsync(User.Identity.Name);
            values.NameSurname = userUpdateViewModel.NameSurname;
            //userUpdateViewModel.ImageUrl = AddProfileImage.ImageAdd(imageFile);
            values.ImageUrl = userUpdateViewModel.ImageUrl;
            values.Email = userUpdateViewModel.Mail;
            values.About = userUpdateViewModel.About;
            if (!userUpdateViewModel.ChangePassword)
            {
                values.PasswordHash = _userManager.PasswordHasher.HashPassword(values, userUpdateViewModel.Password);
            }
            var result = await _userManager.UpdateAsync(values);
            //ViewBag.Cities = _writerCity.GetCityList();
            return RedirectToAction("Index", "Dashboard");
        }
    }
}