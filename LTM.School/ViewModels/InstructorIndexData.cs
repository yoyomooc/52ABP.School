using System.Collections.Generic;
using LTM.School.Core.Models;

namespace LTM.School.ViewModels
{
    public class InstructorIndexData
    {

        public List<Instructor> Instructors { get; set; }

        public List<Course> Courses { get; set; }

        public List<Enrollment> Enrollments { get; set; }



        
    }
}