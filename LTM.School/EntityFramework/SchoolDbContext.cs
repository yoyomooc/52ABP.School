using System;
using LTM.School.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace LTM.School.EntityFramework
{
    public class SchoolDbContext:DbContext
    {
        public SchoolDbContext(DbContextOptions<SchoolDbContext> options) : base(options)
        {
            
        }

        public DbSet<Course> Courses { get; set; }

        public DbSet<Enrollment> Enrollments { get; set; }

        public DbSet<Student> Students { get; set; }

        public DbSet<CourseAssignment> CourseAssignments { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Instructor> Instructors { get; set; }

        public DbSet<OfficeAssignment> OfficeAssignments { get; set; }
      

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>().ToTable("Student");
            modelBuilder.Entity<Enrollment>().ToTable("Enrollment");
            modelBuilder.Entity<Course>().ToTable("Course").Property(a=>a.CourseId).ValueGeneratedNever();
            modelBuilder.Entity<CourseAssignment>().ToTable("CourseAssignment");
            modelBuilder.Entity<Department>().ToTable("Department");
            modelBuilder.Entity<Instructor>().ToTable("Instructor").HasKey(a=>a.Id);
            modelBuilder.Entity<OfficeAssignment>().ToTable("OfficeAssignment").HasKey(a=>a.InstructorId);
               


            modelBuilder.Entity<CourseAssignment>().HasKey(a => new {a.CourseId, a.InstructorId});
        }







    }
}