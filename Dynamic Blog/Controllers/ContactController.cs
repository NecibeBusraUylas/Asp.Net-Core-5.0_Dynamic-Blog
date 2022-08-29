using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreDemo.Controllers
{
    public class ContactController : Controller
    {
        ContactManager contactManager = new ContactManager(new EFContactRepository());

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(Contact contact)
        {  
                contact.ContactDate = DateTime.Parse(DateTime.Now.ToShortDateString());
                contact.ContactStatus = true;
                contactManager.TAdd(contact);
                return RedirectToAction("Index","Blog");
        }
    }
}