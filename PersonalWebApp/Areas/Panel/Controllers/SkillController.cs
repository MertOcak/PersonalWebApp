using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalWebApp.Interfaces;
using PersonalWebApp.Models;

namespace PersonalWebApp.Areas.Panel.Controllers
{
    [Authorize]
    [Area("Panel")]
    public class SkillController : Controller
    {
        private readonly IGenericRepository<Skill> skillRepository;

        public SkillController(IGenericRepository<Skill> skillRepository)
        {
            this.skillRepository = skillRepository;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View(skillRepository.GetAll());
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View("SkillCreate");
        }
        [HttpPost]
        public IActionResult Create(Skill model)
        {
            if (ModelState.IsValid)
            {
                skillRepository.Insert(model);
                skillRepository.Save();
                TempData["Success"] = "Operation Successfull";
                return RedirectToAction("Index");
            }
            return View("CreateSkill", model);
        }
        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            return View("SkillEdit", skillRepository.GetById(id));
        }
        [HttpPost]
        public IActionResult Edit(Skill model)
        {
            if (ModelState.IsValid)
            {
                skillRepository.Update(model);
                skillRepository.Save();
                TempData["Success"] = "Operation Successful";
                return RedirectToAction("Index");
            }

            return View("SkillEdit", model);
        }
        [HttpGet]
        public IActionResult Delete(Guid id)
        {
            skillRepository.Delete(id);
            skillRepository.Save();
            TempData["Success"] = "Deleted Successfully";
            return RedirectToAction("Index");
        }
    }
}