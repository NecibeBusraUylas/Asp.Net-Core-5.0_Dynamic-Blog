using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class CommentManager : ICommentService
    {
        ICommentDal _commentDal;

        public CommentManager(ICommentDal commentDal)
        {
            _commentDal = commentDal;
        }

        public Comment TGetById(int id)
        {
           return  _commentDal.GetById(id);
        }

        public List<Comment> GetList(int id)
        {
            return _commentDal.GetListAll(x => x.BlogId == id);
        }

        public List<Comment> GetList()
        {
            throw new NotImplementedException();
        }

        public void TAdd(Comment t)
        {
            _commentDal.Add(t);
        }

        public void TDelete(Comment t)
        {
            throw new NotImplementedException();
        }

        public void TUpdate(Comment t)
        {
            throw new NotImplementedException();
        }
    }
}