using PersonalWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalWebApp.ViewModels
{
    public class OnePageViewModel
    {
        public IEnumerable<Project> Projects { get; set; }
        public About About { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Education> Educations { get; set; }
    }
}
