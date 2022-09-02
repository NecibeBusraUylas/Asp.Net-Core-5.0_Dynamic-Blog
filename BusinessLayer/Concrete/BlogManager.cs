using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class BlogManager : IBlogService
    {
        private readonly IBlogDal _blogDal;

        public BlogManager(IBlogDal blogDal)
        {
            _blogDal = blogDal;
        }

        public List<Blog> TGetBlogById(int id)
        {
            return _blogDal.GetListAll(x => x.BlogId == id);
        }

        public List<Blog> TGetBlogByWriter(int id)
        {
            return _blogDal.GetListAll(x => x.WriterId == id);
        }

        public List<Blog> TGetBlogListWithCategory()
        {
            return _blogDal.GetListWithCategory();
        }

        public List<Blog> TGetListWithCategoryByWriter(int id)
        {
            return _blogDal.GetListWithCategoryByWriter(id);
        }

        public Blog TGetById(int id)
        {
            return _blogDal.GetById(id);
        }

        public List<Blog> TGetList(Expression<Func<Blog, bool>> filter = null)
        {
            return filter == null ?
                _blogDal.GetListAll() :
                _blogDal.GetListAll(filter);
        }

        public List<Blog> GetLastPosts(int number)
        {
            return _blogDal.GetListAll().TakeLast(number).ToList();
        }
        
        public void TAdd(Blog t)
        {
            _blogDal.Add(t);
        }

        public void TDelete(Blog t)
        {
            _blogDal.Delete(t);
        }

        public void TUpdate(Blog t)
        {
            _blogDal.Update(t);
        }

        public Blog TGetByFilter(Expression<Func<Blog, bool>> filter = null)
        {
            return filter == null ?
                _blogDal.GetByFilter() :
                _blogDal.GetByFilter(filter);
        }

        public int TGetCount(Expression<Func<Blog, bool>> filter = null)
        {
            return filter == null ?
               _blogDal.GetCount() :
               _blogDal.GetCount();
        }

        public List<Blog> TGetLastBlogs(int count)
        {
            return _blogDal.GetListAll().TakeLast(count).ToList();
        }
    }
}