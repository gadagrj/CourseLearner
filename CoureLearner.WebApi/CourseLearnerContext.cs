using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoureLearner.WebApi.Models;

namespace CoureLearner.WebApi
{
    public class CourseLearnerContext : DbContext
    {
        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseCategory> Course_Category { get; set; }
        public DbSet<CourseUserDetail> CourseUser { get; set; }
        public DbSet<CourseEnorlled> CourseEnrollment { get; set; }
        public DbSet<CourseAssets> CourseAssets { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<IncludeMetadataConvention>();
        }
        

    }

}
