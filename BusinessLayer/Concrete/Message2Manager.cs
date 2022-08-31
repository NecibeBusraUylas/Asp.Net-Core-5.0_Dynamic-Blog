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
    public class Message2Manager : IMessage2Service
    {
        private readonly IMessage2Dal _message2Dal;

        public Message2Manager(IMessage2Dal message2Dal)
        {
            _message2Dal = message2Dal;
        }

        public List<Message2> TGetList(Expression<Func<Message2, bool>> filter = null)
        {
            return filter == null ?
               _message2Dal.GetListAll() :
               _message2Dal.GetListAll(filter);
        }

        public void TAdd(Message2 t)
        {
            _message2Dal.Add(t);
        }

        public void TDelete(Message2 t)
        {
            _message2Dal.Delete(t);
        }

        public Message2 TGetByFilter(Expression<Func<Message2, bool>> filter = null)
        {
            return filter == null ?
                 _message2Dal.GetByFilter() :
                 _message2Dal.GetByFilter(filter);
        }

        public Message2 TGetById(int id)
        {
            return _message2Dal.GetById(id);
        }

        public void TUpdate(Message2 t)
        {
            _message2Dal.Update(t);
        }

        public List<Message2> TGetReceivingMessageListByWriter(int id)
        {
            return _message2Dal.GetReceivingMessageListByWriter(id);
        }

        public int TGetCount(Expression<Func<Message2, bool>> filter = null)
        {
            return filter == null ?
               _message2Dal.GetCount() :
               _message2Dal.GetCount();
        }
    }
}