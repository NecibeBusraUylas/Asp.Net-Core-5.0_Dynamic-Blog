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
    public class AdminManager : IAdminService
    {
        private readonly IAdminDal _adminDal;

        public AdminManager(IAdminDal adminDal)
        {
            _adminDal = adminDal;
        }

        public Admin TGetById(int id)
        {
            return _adminDal.GetById(id);
        }

        public List<Admin> TGetList(Expression<Func<Admin, bool>> filter = null)
        {
            return filter == null ?
                _adminDal.GetListAll() :
                _adminDal.GetListAll(filter);
        }

        public List<Admin> GetLastPosts(int number)
        {
            return _adminDal.GetListAll().TakeLast(number).ToList();
        }

        public void TAdd(Admin t)
        {
            _adminDal.Add(t);
        }

        public void TDelete(Admin t)
        {
            _adminDal.Delete(t);
        }

        public void TUpdate(Admin t)
        {
            _adminDal.Update(t);
        }

        public Admin TGetByFilter(Expression<Func<Admin, bool>> filter = null)
        {
            return filter == null ?
                _adminDal.GetByFilter() :
                _adminDal.GetByFilter(filter);
        }

        public int TGetCount(Expression<Func<Admin, bool>> filter = null)
        {
            return filter == null ?
                _adminDal.GetCount() :
                _adminDal.GetCount();
        }
    }
}