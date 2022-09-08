using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DynamicBlog.Models
{
    public class RoleViewModel
    {
        [Required(ErrorMessage = "Lütfen rol adı giriniz.")]
        public string Name { get; set; }
    }
}
