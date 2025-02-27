using doanNet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace doanNet.ApiControllers
{
    public class CategoryController : ApiController
    {
        KTXTDTUEntities2 db = new KTXTDTUEntities2();
        [HttpGet]
        public List<Category> GetAll()
        {
            return db.Categories.ToList();
        }
        [HttpGet]
        public Category GetCategoryByID(int id)
        {
            return db.Categories.Where(row => row.IDCategory == id).FirstOrDefault();
        }
        public Category PostCategory(Category Category)
        {
            Category.DateBegin= DateTime.Now;
            Category.Hide = 0;
            db.Categories.Add(Category);
            db.SaveChangesAsync();
            return Category;
        }
        public Category PutCategory(Category Category)
        {
            Category updateCategory = db.Categories.Where(row => row.IDCategory == Category.IDCategory).FirstOrDefault();
            updateCategory.CategoryTitle = Category.CategoryTitle;
            updateCategory.ColorChip = Category.ColorChip;
            updateCategory.DateBegin = DateTime.Now;
            db.SaveChangesAsync();
            return updateCategory;
        }
        [HttpPost]
        public Category ChangeStatusCategory(Category Category)
        {
            Category hidePriorirty = db.Categories.Where(row => row.IDCategory == Category.IDCategory).FirstOrDefault();
            hidePriorirty.Hide = hidePriorirty.Hide == 0 ? 1 : 0;
            db.SaveChangesAsync();
            return hidePriorirty;
        }
    }
}
