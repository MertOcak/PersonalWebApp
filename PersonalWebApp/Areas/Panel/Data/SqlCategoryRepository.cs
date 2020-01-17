using PersonalWebApp.Areas.Panel.Models;
using PersonalWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalWebApp.Areas.Panel.Data
{
    public class SqlCategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext context;

        public SqlCategoryRepository(AppDbContext context)
        {
            this.context = context;
        }
        public Category Add(Category category)
        {
            context.Add(category);
            context.SaveChanges();
            return category;
        }

        public Category Delete(int id)
        {
            Category category = context.Categories.Find(id);
            if(category != null)
            {
                context.Categories.Remove(category);
                context.SaveChanges();
            }
            return category;
        }

        public IEnumerable<Category> GetAllCategories()
        {
            return context.Categories;
        }

        public Category GetCategory(int id)
        {
            return context.Categories.Find(id);
        }

        public Category Update(Category categoryChanges)
        {
            var category = context.Categories.Attach(categoryChanges);
            category.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return categoryChanges;
        }
    }
}
