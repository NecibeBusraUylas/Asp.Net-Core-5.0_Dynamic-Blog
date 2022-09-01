using BusinessLayer.Abstract;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DynamicBlog.Controllers
{
    [AllowAnonymous]
    public class NewsLetterController : Controller
    {
        private readonly INewsLetterService _newsLetterService;

        [HttpGet]
        public IActionResult SubscribeMail()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SubscribeMail(NewsLetter newsLetter)
        {
            if (_newsLetterService.TGetByMail(newsLetter.Mail) == null)
            {
                newsLetter.MailStatus = true;
                _newsLetterService.TAdd(newsLetter);
                return Ok();
            }
            return BadRequest();
        }
    }
}