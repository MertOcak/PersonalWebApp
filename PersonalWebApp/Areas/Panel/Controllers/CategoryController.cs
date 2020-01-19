using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using PersonalWebApp.Areas.Panel.Data;
using PersonalWebApp.Areas.Panel.Models;
using PersonalWebApp.Data.ProjectData;

namespace PersonalWebApp.Areas.Panel.Controllers
{
    [Area("Panel")]
    [Authorize]
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly IProjectRepository projectRepository;
        private readonly IHostingEnvironment hostingEnvironment;

        public CategoryController(ICategoryRepository categoryRepository, IProjectRepository projectRepository, IHostingEnvironment hostingEnvironment)
        {
            this.categoryRepository = categoryRepository;
            this.projectRepository = projectRepository;
            this.hostingEnvironment = hostingEnvironment;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View(categoryRepository.GetAllCategories());
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View("CategoryCreate");
        }
        [HttpPost]
        public IActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                categoryRepository.Add(category);
                TempData["Success"] = "Operation Successful!";
                return RedirectToAction("index");
            }
            else
            {
                return View("CategoryCreate", category);
            }
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            Category category = categoryRepository.GetCategory(id);
            return View("CategoryEdit", category);
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            categoryRepository.Delete(id);
            TempData["Success"] = "Operation Successful!";
            return RedirectToAction("index");
        }
        [HttpPost]
        public IActionResult Update(Category category)
        {
            if (ModelState.IsValid)
            {
                categoryRepository.Update(category);
                TempData["Success"] = "Operation Successful!";
                return RedirectToAction("index");
            }
            else
            {
                return View("ProjectEdit", category);
            }
        }
    }
}