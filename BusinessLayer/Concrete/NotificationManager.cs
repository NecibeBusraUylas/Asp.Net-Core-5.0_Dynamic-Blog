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
    public class NotificationManager : INotificationService
    {
        private readonly INotificationDal _notificationDal;


        public NotificationManager(INotificationDal notificationDal)
        {
            _notificationDal = notificationDal;
        }

        public List<Notification> TGetList(Expression<Func<Notification, bool>> filter = null)
        {
            return filter == null ?
                _notificationDal.GetListAll() :
                _notificationDal.GetListAll(filter);
        }

        public void TAdd(Notification t)
        {
            _notificationDal.Add(t);
        }

        public void TDelete(Notification t)
        {
            _notificationDal.Delete(t);
        }

        public Notification TGetByFilter(Expression<Func<Notification, bool>> filter = null)
        {
            return filter == null ?
                 _notificationDal.GetByFilter() :
                 _notificationDal.GetByFilter(filter);
        }
        public int GetCount(Expression<Func<Notification, bool>> filter = null)
        {
            return _notificationDal.GetCount(filter);
        }

        public Notification TGetById(int id)
        {
            return _notificationDal.GetById(id);
        }

        public void TUpdate(Notification t)
        {
            _notificationDal.Update(t);
        }

        public int TGetCount(Expression<Func<Notification, bool>> filter = null)
        {
            return filter == null ?
               _notificationDal.GetCount() :
               _notificationDal.GetCount();
        }
    }
}