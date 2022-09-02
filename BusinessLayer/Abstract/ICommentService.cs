using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface ICommentService: IGenericService<Comment>
    {
        //void AddComment(Comment comment);
        //void DeleteComment(Category comment);
        //void UpdateComment(Comment comment);
        List<Comment> TGetList(int id);
        //Comment GetById(int id);
    }
}