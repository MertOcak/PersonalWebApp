using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PersonalWebApp.Models;

namespace PersonalWebApp.Data.ProjectData
{
    public interface IProjectRepository
    {
        Project GetProject(/*string sefUrl*/int id);
        IEnumerable<Project> GetAllProjects();
        Project Add(Project project);
        Project Update(Project projectChanges);
        Project Delete(int id);
    }
}
