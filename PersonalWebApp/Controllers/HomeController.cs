using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PersonalWebApp.Data.ProjectData;

namespace PersonalWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProjectRepository projectRepository;

        public HomeController(IProjectRepository projectRepository)
        {
            this.projectRepository = projectRepository;
        }
        public IActionResult Index()
        {
            //return RedirectToAction("index", "project",new { area = "panel" });
            return View(projectRepository.GetAllProjects());
        }
    }
}