using DynamicBlog.Models;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DynamicBlog.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;

        public LoginController(SignInManager<AppUser> signInManager)
        {
            _signInManager = signInManager;
        }
     
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        //[HttpPost]
        //public async Task<IActionResult> Index(Writer writer)
        //{
        //    var dataValue = _writerService.TGetByFilter(x => x.WriterMail == writer.WriterMail && x.WriterPassword == writer.WriterPassword);
        //    if (dataValue != null)

        //    {
        //        //      HttpContext.Session.SetString("username", p.WriterMail); //key-value pair for session
        //        var claims = new List<Claim>
        //        {
        //            new Claim(ClaimTypes.Email, dataValue.WriterMail),
        //            new Claim(ClaimTypes.Name,  dataValue.WriterId.ToString())
        //        };
        //        //ClaimsIdentity(IEnumerable<Claim>)-> Numaralandırılmış nesne koleksiyonu kullanarak sınıfının yeni bir örneğini ClaimsIdentity Claim başlatır.
        //        //ClaimsIdentity(IEnumerable<Claim>, String)-> Belirtilen talepler ve kimlik doğrulama türüyle sınıfının yeni bir örneğini ClaimsIdentity başlatır.
        //        var useridentity = new ClaimsIdentity(claims,"a");
        //        ClaimsPrincipal principal = new ClaimsPrincipal(useridentity);
        //        await HttpContext.SignInAsync(principal); //şifreli formatta cookie oluşturmak için
        //        return RedirectToAction("Index", "Dashboard");
        //    }
        //    else
        //    {
        //        TempData["ErrorMessage"] = "Kullanıcı adı veya şifreniz yanlış";
        //        return View();
        //    }
        //}

        [HttpPost]
        public async Task<IActionResult> Index(UserSignInViewModel appUser)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(appUser.UserName, appUser.Password, appUser.IsPersistent, true);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Dashboard");
                }
                else
                {
                    TempData["ErrorMessage"] = "Kullanıcı adınız veya parolanız hatalı lütfen tekrar deneyiniz.";
                    return View(appUser);
                }
            }
            return View(appUser);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Blog");
        }
    }
}