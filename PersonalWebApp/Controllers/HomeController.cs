using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PersonalWebApp.Interfaces;
using PersonalWebApp.Models;

namespace PersonalWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IGenericRepository<Project> projectRepository;

        public HomeController(IGenericRepository<Project> projectRepository)
        {
            this.projectRepository = projectRepository;
        }
        public IActionResult Index()
        {
            //return RedirectToAction("index", "project",new { area = "panel" });
            return View(projectRepository.GetAll());
        }
    }
}