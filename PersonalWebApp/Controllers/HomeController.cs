using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PersonalWebApp.Interfaces;
using PersonalWebApp.Models;
using PersonalWebApp.ViewModels;

namespace PersonalWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IGenericRepository<Project> projectRepository;
        private readonly IGenericRepository<About> aboutRepository;
        private readonly IGenericRepository<Category> categoryRepository;
        private readonly IGenericRepository<Education> educationRepository;

        public HomeController(IGenericRepository<Project> projectRepository, IGenericRepository<About> aboutRepository, IGenericRepository<Category> categoryRepository, IGenericRepository<Education> educationRepository)
        {
            this.projectRepository = projectRepository;
            this.aboutRepository = aboutRepository;
            this.categoryRepository = categoryRepository;
            this.educationRepository = educationRepository;
        }
        public IActionResult Index()
        {
            OnePageViewModel onePage = new OnePageViewModel {
                Projects = projectRepository.GetAll(),
                About = aboutRepository.GetById(Guid.Parse("3d6257bf-227d-4f35-9d13-369205940242")),
                Categories = categoryRepository.GetAll(),
                Educations = educationRepository.GetAll()
            };

            //return RedirectToAction("index", "project",new { area = "panel" });
            return View(onePage);
        }
    }
}