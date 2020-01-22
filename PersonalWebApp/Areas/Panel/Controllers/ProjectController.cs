﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        private readonly AppDbContext context;
        private readonly ICategoryRepository categoryRepository;
        private readonly IProjectRepository projectRepository;
        private readonly IHostingEnvironment hostingEnvironment;

        public ProjectController(AppDbContext context, ICategoryRepository categoryRepository, IProjectRepository projectRepository, IHostingEnvironment hostingEnvironment)
        {
            this.context = context;
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
            return View("ProjectCreate", data);
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
            }
            else
            {
                return View("ProjectCreate", data);
            }
        }
        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            ProjectEditViewModel data = new ProjectEditViewModel();
            data.Categories = categoryRepository.GetAllCategories();

            var categoriesImport = context.Projects
   .Include(x => x.ProjectCategories).ThenInclude(x => x.Category);

            var selectedCategories = categoriesImport.Where(x => x.Id == id).Select(x => x.ProjectCategories).First().ToList();


            for (int i = 0; i < selectedCategories.Count(); i++)
            {
                foreach (var category in data.Categories)
                {
                    if (category.Id == selectedCategories[i].CategoryId)
                    {
                        category.IsChecked = true;
                    }

                }
            }

            var images = context.Projects.Include(p => p.ProjectImages).ThenInclude(p=>p.Image).ToList();

            List<string> projectImages = new List<string>();
            foreach (Project p in images)
            {
                var filter = p.ProjectImages;

                foreach (var pi in filter)
                {
                  if(pi.ProjectId == id)
                    {

                            projectImages.Add(pi.Image.ImagePath);

                    }
                }
            }

            data.ImagePath = projectImages;


            //var allProjects = context.Projects.Include(p => p.ProjectCategories).ToList();

            //   List<Project> categoryRelatedProjects = new List<Project>();
            //   foreach (Project p in allProjects)
            //   {
            //       var filter = p.ProjectCategories;

            //       foreach (var pr in filter)
            //       {
            //           if (pr.CategoryId.ToString() == "83278a7e-2a50-4914-62c9-08d79ca3d529")
            //           {
            //               categoryRelatedProjects.Add(p);
            //           }
            //       }
            //   }



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
        public IActionResult Update(ProjectEditViewModel data, Guid[] categoryList)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = ProcessUploadedFile(data);

                Project project = new Project();
                project.Id = data.Project.Id;
                project.Title = data.Project.Title;
                project.Description = data.Project.Description;
                if (uniqueFileName != null)
                {
                    project.PhotoPath = uniqueFileName;
                }
                else
                {
                    project.PhotoPath = context.Projects.AsNoTracking().Where(p => p.Id == data.Project.Id).First().PhotoPath;
                }

                projectRepository.Update(project);


                var currentProject = context.Projects.Include(p => p.ProjectCategories).Single(p => p.Id == data.Project.Id);



                //foreach (var category in currentProject.ProjectCategories)
                //{
                //    currentProject.ProjectCategories.Remove(category);
                //}
                //context.SaveChanges();


                //Delete Categories
                foreach (var item in currentProject.ProjectCategories.ToList())
                {
                    currentProject.ProjectCategories.Remove(item);
                    context.SaveChanges();
                }

                for (int i = 0; i < categoryList.Length; i++)
                {

                    var category = context.Categories
                    .Single(p => p.Id == categoryList[i]);



                    //bool exists = context.Entry(category)
                    // .Collection(m => m.ProjectCategories)
                    // .Query()
                    // .Any(x => x.CategoryId == categoryList[i]);



                    //if (exists)
                    //    continue;



                    // Add Categories
                    currentProject.ProjectCategories.Add(new ProjectCategory
                    {
                        Project = currentProject,
                        Category = category
                    });
                    context.SaveChanges();
                }



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

                    var currentProject = context.Projects.Include(p => p.ProjectCategories).Single(p => p.Id == model.Project.Id);


                    Image image = new Image();
                    image.ImageId = Guid.NewGuid();
                    image.ImagePath = uniqueFileName;

                    ProjectImages newImage = new ProjectImages();
                    newImage.Project = currentProject;
                    newImage.Image = image;

                    context.Projects.Include(p => p.ProjectImages).Single(p => p.Id == model.Project.Id).ProjectImages.Add(newImage);
                    context.SaveChanges();

                }

            }

            return uniqueFileName;
        }
    }
}