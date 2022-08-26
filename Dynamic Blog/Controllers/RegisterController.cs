using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.EntityFramework;
using DynamicBlog.Models;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
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
        WriterManager wm = new WriterManager(new EFWriterRepository());

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Index()
        {
            WriterAndCity writersAndCities = new WriterAndCity();
            writersAndCities.Cities = GetCityList();
            return View(writersAndCities);
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Index(WriterAndCity writersAndCities, string passwordAgain, string cities)
        {
            WriterValidator wv = new WriterValidator();
            ValidationResult results = wv.Validate(writersAndCities.Writer);
            if (results.IsValid && writersAndCities.Writer.WriterPassword == passwordAgain)
            {
                writersAndCities.Writer.WriterStatus = true;
                writersAndCities.Writer.WriterAbout = "Deneme test";
                wm.AddWriter(writersAndCities.Writer);
                return RedirectToAction("Index", "Blog");
            }
            else if (!results.IsValid)
            {
                foreach (var item in results.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }
            else if(writersAndCities.Writer.WriterPassword != passwordAgain)
            {
                ModelState.AddModelError("WriterPassword", "Girdiğiniz parolalar eşleşmedi lütfen tekrar deneyin");
            }
            writersAndCities.Cities = GetCityList();

            return View(writersAndCities);
        }

        public List<SelectListItem> GetCityList()
        {
            List<SelectListItem> cities = (from x in GetCityArray()
                                              select new SelectListItem
                                              {
                                                  Text = x,
                                                  Value = x
                                              }).ToList();
            return cities;
        }

        public List<string> GetCityArray() //şehirleri array şeklinde alıp bunu listeye çevirdim
        {
            String[] CitiesArray = new String[] { "Adana", "Adıyaman", "Afyon", "Ağrı", "Aksaray", "Amasya", "Ankara", "Antalya", "Ardahan", "Artvin", "Aydın", "Bartın", "Batman", "Balıkesir", "Bayburt", "Bilecik", "Bingöl", "Bitlis", "Bolu", "Burdur", "Bursa", "Çanakkale", "Çankırı", "Çorum", "Denizli", "Diyarbakır", "Düzce", "Edirne", "Elazığ", "Erzincan", "Erzurum", "Eskişehir", "Gaziantep", "Giresun", "Gümüşhane", "Hakkari", "Hatay", "Iğdır", "Isparta", "İçel", "İstanbul", "İzmir", "Karabük", "Karaman", "Kars", "Kastamonu", "Kayseri", "Kırıkkale", "Kırklareli", "Kırşehir", "Kilis", "Kocaeli", "Konya", "Kütahya", "Malatya", "Manisa", "Kahramanmaraş", "Mardin", "Muğla", "Muş", "Nevşehir", "Niğde", "Ordu", "Osmaniye", "Rize", "Sakarya", "Samsun", "Siirt", "Sinop", "Sivas", "Tekirdağ", "Tokat", "Trabzon", "Tunceli", "Şanlıurfa", "Şırnak", "Uşak", "Van", "Yalova", "Yozgat", "Zonguldak" };
            return new List<string>(CitiesArray);
        }
    }
}