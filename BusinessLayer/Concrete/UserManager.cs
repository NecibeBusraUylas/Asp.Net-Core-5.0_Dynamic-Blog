using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class UserManager : IUserService
    {
        IUserDal _userDal;

        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        public void TAdd(AppUser t)
        {
            _userDal.Add(t);
        }

        public void TDelete(AppUser t)
        {
            _userDal.Delete(t);
        }

        public AppUser TGetByFilter(Expression<Func<AppUser, bool>> filter = null)
        {
            return filter == null ?
                _userDal.GetByFilter() :
                _userDal.GetByFilter(filter);
        }

        public AppUser TGetById(int id)
        {
            return _userDal.GetById(id);
        }

        public int TGetCount(Expression<Func<AppUser, bool>> filter = null)
        {
            return filter == null ?
               _userDal.GetCount() :
               _userDal.GetCount();
        }

        public List<AppUser> TGetList(Expression<Func<AppUser, bool>> filter = null)
        {
            return filter == null ?
                _userDal.GetListAll() :
                _userDal.GetListAll(filter);
        }

        public void TUpdate(AppUser t)
        {
            _userDal.Update(t);
        }
    }
}