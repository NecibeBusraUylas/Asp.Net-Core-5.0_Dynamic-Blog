using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class WriterRepository : IGenericDal<Writer>
    {
        Context c = new Context();

        public void Add(Writer t)
        {
            c.Add(t);
            c.SaveChanges();
        }

        public void Delete(Writer t)
        {
            c.Remove(t);
            c.SaveChanges();
        }

        public Writer GetById(int id)
        {
            return c.Set<Writer>().Find(id);
        }

        public List<Writer> GetListAll()
        {
            return c.Set<Writer>().ToList();
        }

        public void Update(Writer t)
        {
            c.Update(t);
            c.SaveChanges();
        }
    }
}