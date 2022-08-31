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
    public class WriterManager : IWriterService
    {
        private readonly IWriterDal _writerDal;

        public WriterManager(IWriterDal writerDal)
        {
            _writerDal = writerDal;
        }

        public Writer TGetById(int id)
        {
            return _writerDal.GetById(id);
        }

        public void TAdd(Writer t)
        {
            _writerDal.Add(t);
        }

        public void TDelete(Writer t)
        {
            _writerDal.Delete(t);
        }

        public void TUpdate(Writer t)
        {
            _writerDal.Update(t);
        }

        public List<Writer> GetList(Expression<Func<Writer, bool>> filter = null)
        {
            return _writerDal.GetListAll(filter);
        }

        public int TGetCount(Expression<Func<Writer, bool>> filter = null)
        {
            return filter == null ?
                _writerDal.GetCount() :
                _writerDal.GetCount();
        }


        public List<Writer> GetWriterById(int id)
        {
            return _writerDal.GetListAll(x => x.WriterId == id);
        }

        public Writer TGetByFilter(Expression<Func<Writer, bool>> filter)
        {
            return filter == null ?
                _writerDal.GetByFilter() :
                _writerDal.GetByFilter(filter);
        }

        public List<Writer> TGetList(Expression<Func<Writer, bool>> filter = null)
        {
            return filter == null ?
                 _writerDal.GetListAll() :
                 _writerDal.GetListAll(filter);
        }
    }
}