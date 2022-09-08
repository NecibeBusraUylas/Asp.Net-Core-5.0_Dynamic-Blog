using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DynamicBlog.Areas.Admin.Models
{
    public class RoleAssignViewModel
    {
        public int RoleId { get; set; }
        public string Name { get; set; }
        public bool Exists { get; set; } // kullanıcıya rol tanımlıise 1 değilse 0 
    }
} 