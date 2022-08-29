using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DynamicBlog.Models
{
    public class AddProfileImage
    {
        public string ImageAdd(IFormFile image, out string fileName)
        {
            var imageLocation = "";
            var extension = Path.GetExtension(image.FileName);
            var newImageName = Guid.NewGuid() + extension;
            var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/WriterImageFiles/",
                newImageName);
            var stream = new FileStream(location, FileMode.Create);
            image.CopyTo(stream);
            fileName = image.FileName;
            return imageLocation= "\\WriterImageFiles\\" + newImageName;
        }
    }
}