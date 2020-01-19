using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PersonalWebApp.Areas.Panel.Data;
using PersonalWebApp.Areas.Panel.Models;
using PersonalWebApp.Areas.Panel.ViewModels;
using PersonalWebApp.Data.ProjectData;
using PersonalWebApp.Models;

namespace PersonalWebApp.Areas.Panel.Controllers
{
    [Area("Panel")]
    [Authorize]
    public class ProjectController : Controller
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly IProjectRepository projectRepository;
        private readonly IHostingEnvironment hostingEnvironment;

        public ProjectController(ICategoryRepository categoryRepository, IProjectRepository projectRepository, IHostingEnvironment hostingEnvironment)
        {
            this.categoryRepository = categoryRepository;
            this.projectRepository = projectRepository;
            this.hostingEnvironment = hostingEnvironment;
        }
        [HttpGet]
        public IActionResult Index()
        {
            ProjectListViewModel data = new ProjectListViewModel();
            data.Projects = projectRepository.GetAllProjects();
            data.Categories = categoryRepository.GetAllCategories();
            return View(data);
        }
        [HttpGet]
        public IActionResult Create()
        {
            ProjectEditViewModel data = new ProjectEditViewModel();
            data.Categories = categoryRepository.GetAllCategories();
            return View("ProjectCreate",data);
        }
        [HttpPost]
        public IActionResult Create(ProjectEditViewModel data)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = ProcessUploadedFile(data);
                Project project = new Project();
                project.Title = data.Project.Title;
                project.Description = data.Project.Description;
                project.PhotoPath = uniqueFileName;
                projectRepository.Add(project);
                TempData["Success"] = "Operation Successful!";
                return RedirectToAction("index");
            } else
            {
                return View("ProjectCreate", data);
            }
        }
        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            ProjectEditViewModel data = new ProjectEditViewModel();
            data.Categories = categoryRepository.GetAllCategories();
            data.Project = projectRepository.GetProject(id);
            return View("ProjectEdit", data);
        }
        [HttpGet]
        public IActionResult Delete(Guid id)
        {
            projectRepository.Delete(id);
            TempData["Success"] = "Operation Successful!";
            return RedirectToAction("index");
        }
        [HttpPost]
        public IActionResult Update(ProjectEditViewModel data)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = ProcessUploadedFile(data);
                Project project = new Project();
                project.Id = data.Project.Id;
                project.Title = data.Project.Title;
                project.Description = data.Project.Description;
                project.PhotoPath = uniqueFileName;
                projectRepository.Update(project);
                TempData["Success"] = "Operation Successful!";
                return RedirectToAction("index");
            }
            else
            {
                return View("ProjectEdit", data);
            }
        }
        private string ProcessUploadedFile(ProjectEditViewModel model)
        {
            string uniqueFileName = null;
            if (model.Images != null && model.Images.Count > 0)
            {
                foreach (IFormFile photo in model.Images)
                {
                    string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "userdata/projects");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + photo.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        photo.CopyTo(fileStream);
                    }
                }
            }

            return uniqueFileName;
        }
    }
}