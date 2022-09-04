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
    public class NewsLetterManager : INewsLetterService
    {
        private readonly INewsLetterDal _newsLetterDal;

        public NewsLetterManager(INewsLetterDal newsLetterDal)
        {
            _newsLetterDal = newsLetterDal;
        }

        public NewsLetter TGetById(int id)
        {
            return _newsLetterDal.GetById(id);
        }

        public void TAdd(NewsLetter t)
        {
            t.MailStatus = true;
            _newsLetterDal.Add(t);
        }

        public void TDelete(NewsLetter t)
        {
            _newsLetterDal.Delete(t);
        }

        public void TUpdate(NewsLetter t)
        {
            _newsLetterDal.Update(t);
        }

        public NewsLetter TGetByFilter(Expression<Func<NewsLetter, bool>> filter = null)
        {
            return filter == null ?
                 _newsLetterDal.GetByFilter() :
                 _newsLetterDal.GetByFilter(filter);
        }

        public List<NewsLetter> TGetList(Expression<Func<NewsLetter, bool>> filter = null)
        {
            return filter == null ?
                _newsLetterDal.GetListAll() :
                _newsLetterDal.GetListAll(filter);
        }

        public int TGetCount(Expression<Func<NewsLetter, bool>> filter = null)
        {
            return filter == null ?
               _newsLetterDal.GetCount() :
               _newsLetterDal.GetCount();
        }

        public NewsLetter TGetByMail(string mail)
        {
            return _newsLetterDal.GetByFilter(x => x.Mail == mail);
        }
    }
}