using PersonalWebApp.Areas.Panel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalWebApp.Areas.Panel.Data
{
    public interface ICategoryRepository
    {
        Category GetCategory(int Id);
        IEnumerable<Category> GetAllCategories();
        Category Add(Category category);
        Category Update(Category categoryChanges);
        Category Delete(int id);
    }
}
