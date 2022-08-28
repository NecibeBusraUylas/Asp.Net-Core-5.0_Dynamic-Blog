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
    public class AboutManager : IAboutServive
    {
        IAboutDal _aboutDal;

        public AboutManager(IAboutDal aboutDal)
        {
            _aboutDal = aboutDal;
        }

        public void TAdd(About t)
        {
            throw new NotImplementedException();
        }

        public void TDelete(About t)
        {
            throw new NotImplementedException();
        }

        public About TGetById(int id)
        {
            throw new NotImplementedException();
        }

        public List<About> TGetList(Expression<Func<About, bool>> filter)
        {
            return filter == null ?
                _aboutDal.GetListAll() :
                _aboutDal.GetListAll(filter);
        }

        public void TUpdate(About t)
        {
            throw new NotImplementedException();
        }

        public About TGetByFilter(Expression<Func<About, bool>> filter)
        {
            return filter == null ?
                _aboutDal.GetByFilter() :
                _aboutDal.GetByFilter(filter);
        }
    }
}