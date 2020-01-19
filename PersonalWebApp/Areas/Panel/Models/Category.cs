using PersonalWebApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalWebApp.Areas.Panel.Models
{
    public class Category
    {
        [Column("CategoryId")]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Category name is required")]
        public string CategoryName { get; set; }
        public bool IsChecked { get; set; }
        public bool IsActive { get; set; }

        // Relationships Many to Many for Categories
        public ICollection<ProjectCategory> ProjectCategories { get; set; }
    }
}
