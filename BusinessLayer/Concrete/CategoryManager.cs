using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
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

        ICategoryDal _categoryDal;

        public CategoryManager(ICategoryDal categoryDal)
        {
            _categoryDal = categoryDal;
        }

        public void AddCategory(Category category)
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
            _categoryDal.Add(category);
        }

        public void DeleteCategory(Category category)
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
            _categoryDal.Delete(category);
        }

        public Category GetById(int id)
        {
            // return categoryRepository.GetById(id);
            return _categoryDal.GetById(id);
        }

        public List<Category> GetList()
        {
            //return categoryRepository.GetListAll();
            return _categoryDal.GetListAll();
        }

        public void UpdateCategory(Category category)
        {
            //categoryRepository.Update(category);
            _categoryDal.Update(category);
        }
    }
}