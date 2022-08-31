using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class CategoryManager : ICategoryService
    {
        //EFCategoryRepository categoryRepository;
        //public CategoryManager()
        //{
        //    categoryRepository = new EFCategoryRepository();
        //}

        private readonly ICategoryDal _categoryDal;

        public CategoryManager(ICategoryDal categoryDal)
        {
            _categoryDal = categoryDal;
        }

        public void TAdd(Category t)
        {
            //if(category.CategoryName!="" && category.CategoryDescription != "" && 
            //    category.CategoryName.Length>=5 && category.CategoryStatus== true)
            //{
            //    categoryRepository.AddCategory(category);
            //}
            //else
            //{
            //    //hata mesajı
            //}
            //categoryRepository.Add(category);
            _categoryDal.Add(t);
        }

        public void TDelete(Category t)
        {
            //if (category.CategoryId!= 0)
            //{
            //    genericCategory.Delete(category);
            //}
            //else
            //{
            //    //hata mesajı
            //}
            //categoryRepository.Delete(category);
            _categoryDal.Delete(t);
        }

        public Category TGetById(int id)
        {
            // return categoryRepository.GetById(id);
            return _categoryDal.GetById(id);
        }

        public List<Category> TGetList(Expression<Func<Category, bool>> filter = null)
        {
            return filter == null ?
                _categoryDal.GetListAll() :
                _categoryDal.GetListAll(filter);
        }


        public void TUpdate(Category t)
        {
            //categoryRepository.Update(category);
            _categoryDal.Update(t);
        }

        public Category TGetByFilter(Expression<Func<Category, bool>> filter = null)
        {
            return filter == null ?
                 _categoryDal.GetByFilter() :
                 _categoryDal.GetByFilter(filter);
        }

        public int TGetCount(Expression<Func<Category, bool>> filter = null)
        {
            return filter == null ?
               _categoryDal.GetCount() :
               _categoryDal.GetCount();
        }
    }
}