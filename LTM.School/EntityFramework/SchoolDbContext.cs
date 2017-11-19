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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>().ToTable("Student");
            modelBuilder.Entity<Course>().ToTable("Course").Property(a=>a.CourseId).ValueGeneratedNever();

            modelBuilder.Entity<Enrollment>().ToTable("Enrollment");


        }







    }
}