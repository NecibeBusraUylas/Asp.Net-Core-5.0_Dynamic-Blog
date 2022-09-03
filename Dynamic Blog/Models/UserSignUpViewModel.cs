using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DynamicBlog.Models
{
    public class UserSignUpViewModel
    {
        [Display(Name = "Ad Soyad")]
        [Required(ErrorMessage = "Lütfen ad ve soyad giriniz.")]
        public string NameSurname { get; set; }

        [Display(Name = "Parola")]
        [Required(ErrorMessage = "Lütfen parola giriniz.")]
        public string Password { get; set; }

        [Display(Name = "Parola Tekrar")]
        [Compare("Password", ErrorMessage = "Parolalar uyuşmuyor.")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Mail Adresi")]
        [Required(ErrorMessage = "Lütfen e-mail adresinizi giriniz.")]
        public string Mail { get; set; }

        [Display(Name = "Kullanıcı Adı")]
        [Required(ErrorMessage = "Lütfen kullanıcı adınızı giriniz.")]
        public string UserName { get; set; }

        public bool IsAcceptTheContract { get; set; }
    }
}