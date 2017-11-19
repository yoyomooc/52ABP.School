using LTM.School.Application.enumsType;

namespace LTM.School.Core.Models
{
    public class Enrollment
    {
        public int EnrollmentId { get; set; }

        public CourseGrade? Grade { get; set; }

        public int StudentId { get; set; }

        public int CourseId { get; set; }

        public    Student Student { get; set; }

        public    Course Course { get; set; }


     

    }
}