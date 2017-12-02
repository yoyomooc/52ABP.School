using System.ComponentModel.DataAnnotations;
using LTM.School.Application.enumsType;

namespace LTM.School.Core.Models
{
    /// <summary>
    /// 注册信息
    /// </summary>
    public class Enrollment
    {
        public int EnrollmentId { get; set; }

        [DisplayFormat(NullDisplayText = "暂无成绩")]
        public CourseGrade? Grade { get; set; }

        public int StudentId { get; set; }

        public int CourseId { get; set; }

        public    Student Student { get; set; }

        public    Course Course { get; set; }


     

    }
}