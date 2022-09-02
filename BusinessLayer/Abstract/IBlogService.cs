using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IBlogService : IGenericService<Blog>
    {
        //void AddBlog(Blog blog);
        //void DeleteBlog(Blog blog);
        //void UpdateBlog(Blog blog);
        //List<Blog> GetList();
        //Blog GetById(int id);
        List<Blog> TGetBlogListWithCategory();
        List<Blog> TGetBlogById(int id);
        List<Blog> TGetBlogByWriter(int id);
        public List<Blog> TGetListWithCategoryByWriter(int id);
        List<Blog> TGetLastBlogs(int count);
    }
}