using Microsoft.AspNetCore.Http;
using PersonalWebApp.Areas.Panel.Models;
using PersonalWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalWebApp.Areas.Panel.ViewModels
{
    public class ProjectEditViewModel
    {
        public ProjectEditViewModel()
        {
            Categories = new List<Category>();
        }

        public Project Project { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public List<IFormFile> Images { get; set; }

    }
}
