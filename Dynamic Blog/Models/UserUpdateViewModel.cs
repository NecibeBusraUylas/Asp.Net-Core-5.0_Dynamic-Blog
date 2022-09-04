using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DynamicBlog.Models
{
    public class UserUpdateViewModel
    {
        public string NameSurname { get; set; }
        public string Username { get; set; }
        public string Mail { get; set; }
        public string ImageUrl { get; set; }
        public string Password { get; set; }
        public string About { get; set; }
    }
}