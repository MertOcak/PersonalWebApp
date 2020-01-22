//using System;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
//using System.Threading.Tasks;
using PersonalWebApp.Models;

namespace PersonalWebApp.Data.ProjectData
{
    public class SqlProjectRepository : IProjectRepository
    {
        private readonly AppDbContext _context;

        public SqlProjectRepository(AppDbContext context)
        {
            _context = context;
        }

        public Project GetProject(Guid id/*string sefUrl*/)
        {
            //return _context.Projects.FirstOrDefault(p => p.SefUrl == sefUrl);
            return _context.Projects.Find(id);
        }

        public IEnumerable<Project> GetAllProjects()
        {
            return _context.Projects;
        }

        public Project Add(Project project)
        {
            _context.Projects.Add(project);
            _context.SaveChanges();
            return project;
        }

        public Project Update(Project projectChanges)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseSqlServer(("Server=(localdb)\\MSSQLLocalDB;Database=PersonalWebAppDB;Trusted_Connection=True;MultipleActiveResultSets=true"));

            using (var a = new AppDbContext(optionsBuilder.Options))
            {
                var project = a.Projects.Attach(projectChanges);
                project.State = EntityState.Modified;
                a.SaveChanges();
            }

            
            return projectChanges;
        }

        public Project Delete(Guid id)
        {
            Project project = _context.Projects.Find(id);
            if (project != null)
            {
                _context.Projects.Remove(project);
                _context.SaveChanges();
            }

            return project;
        }
    }
}
