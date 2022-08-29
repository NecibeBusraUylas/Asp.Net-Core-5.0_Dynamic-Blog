using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DynamicBlog.Controllers
{
    public class LoginController : Controller
    {
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Index(Writer writer)
        {
            WriterManager writermanager = new WriterManager(new EFWriterRepository());
            var dataValue = writermanager.TGetByFilter(x => x.WriterMail == writer.WriterMail && x.WriterPassword == writer.WriterPassword);
            if (dataValue != null)

            {
                //      HttpContext.Session.SetString("username", p.WriterMail); //key-value pair for session
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, dataValue.WriterMail),
                    new Claim(ClaimTypes.Name,  dataValue.WriterId.ToString())
                };
                //ClaimsIdentity(IEnumerable<Claim>)-> Numaralandırılmış nesne koleksiyonu kullanarak sınıfının yeni bir örneğini ClaimsIdentity Claim başlatır.
                //ClaimsIdentity(IEnumerable<Claim>, String)-> Belirtilen talepler ve kimlik doğrulama türüyle sınıfının yeni bir örneğini ClaimsIdentity başlatır.
                var useridentity = new ClaimsIdentity(claims,"a");
                ClaimsPrincipal principal = new ClaimsPrincipal(useridentity);
                await HttpContext.SignInAsync(principal); //şifreli formatta cookie oluşturmak için
                return RedirectToAction("Index", "Dashboard");
            }
            else
            {
                return View();
            }
        }
    }
}