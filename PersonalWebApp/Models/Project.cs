using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalWebApp.Models
{
    public class Project
    {
        public int Id { get; set; }
        [Display(Name ="Project Title", Description = "Please enter project title here")]
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public string Tag { get; set; }
        public string PhotoPath { get; set; }
        public string ProjectUrl { get; set; }
        public string SefUrl { get; set; }
    }
}
