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
    public class CommentManager : ICommentService
    {
        private readonly ICommentDal _commentDal;

        public CommentManager(ICommentDal commentDal)
        {
            _commentDal = commentDal;
        }

        public Comment TGetById(int id)
        {
           return  _commentDal.GetById(id);
        }

        public List<Comment> TGetListById(int id)
        {
            return _commentDal.GetListAll(x => x.BlogId == id);
        }

        public void TAdd(Comment t)
        {
            _commentDal.Add(t);
        }

        public void TDelete(Comment t)
        {
            _commentDal.Delete(t);
        }

        public void TUpdate(Comment t)
        {
            _commentDal.Update(t);
        }

        public Comment TGetByFilter(Expression<Func<Comment, bool>> filter = null)
        {
            return filter == null ?
                 _commentDal.GetByFilter() :
                 _commentDal.GetByFilter(filter);
        }

        public List<Comment> TGetList(Expression<Func<Comment, bool>> filter = null)
        {
            return filter == null ?
                _commentDal.GetListAll() :
                _commentDal.GetListAll(filter);
        }

        public int TGetCount(Expression<Func<Comment, bool>> filter = null)
        {
            return filter == null ?
               _commentDal.GetCount() :
               _commentDal.GetCount();
        }

        public List<Comment> TGetBlogListWithComment()
        {
            return _commentDal.GetBlogListWithComment();
        }
    }
}