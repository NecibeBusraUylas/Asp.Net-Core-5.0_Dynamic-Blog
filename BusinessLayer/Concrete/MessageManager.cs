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
    public class MessageManager : IMessageService
    {
        IMessageDal _messageDal;

        public MessageManager(IMessageDal messageDal)
        {
            _messageDal = messageDal;
        }

        public List<Message> TGetList(Expression<Func<Message, bool>> filter = null)
        {
            return filter == null ?
                _messageDal.GetListAll() :
                _messageDal.GetListAll(filter);
        }

        public void TAdd(Message t)
        {
            _messageDal.Add(t);
        }

        public void TDelete(Message t)
        {
            _messageDal.Delete(t);
        }

        public Message TGetByFilter(Expression<Func<Message, bool>> filter = null)
        {
            return filter == null ?
                 _messageDal.GetByFilter() :
                 _messageDal.GetByFilter(filter);
        }
  
        public Message TGetById(int id)
        {
            return _messageDal.GetById(id);
        }

        public void TUpdate(Message t)
        {
            _messageDal.Update(t);
        }
    }
}