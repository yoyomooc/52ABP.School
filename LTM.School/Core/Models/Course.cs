using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using LTM.School.Application.enumsType;

namespace LTM.School.Core.Models
{
    public class Course
    {
     //   [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CourseId { get; set; }

        public string Title { get; set; }

        public int Credits { get; set; }

        public CourseGrade Grade { get; set; }

        public ICollection<Enrollment> Enrollments { get; set; }


    }
}