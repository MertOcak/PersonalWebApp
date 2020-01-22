using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PersonalWebApp.Areas.Panel.Models;
using PersonalWebApp.Models.Configurations;

namespace PersonalWebApp.Models
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Project> Projects { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Image> Images { get; set; }

        public DbSet<ProjectImages> ProjectImages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new ProjectCategoryConfiguration());
            modelBuilder.ApplyConfiguration(new ProjectImageConfiguration());

        }

    }
}
