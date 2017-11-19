using System;
using System.Collections.Generic;

namespace LTM.School.Core.Models
{
    /// <summary>
    ///     学生
    /// </summary>
    public class Student
    {
        public int Id { get; set; }

        public string RealName { get; set; }


        public DateTime EnrollmentDate { get; set; }

        public ICollection<Enrollment> Enrollments { get; set; }
    }
}