using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LTM.School.Application.enumsType;

namespace LTM.School.Core.Models
{/// <summary>
/// 课程
/// </summary>
    public class Course
    {
     //   [DatabaseGenerated(DatabaseGeneratedOption.None)]

      [Display(Name = "Number")]
        public int CourseId { get; set; }

        [StringLength(50,MinimumLength = 2)]
        public string Title { get; set; }
        /// <summary>
        /// 评分
        /// </summary>
        [Range(0,5)]
        public int Credits { get; set; }
        /// <summary>
        /// 课程成绩
        /// </summary>
        public CourseGrade Grade { get; set; }

       
        /// <summary>
        /// 部门id
        /// </summary>
        public int DepartmentId { get; set; }


        public ICollection<CourseAssignment> CourseAssignments { get; set; }

        public ICollection<Enrollment> Enrollments { get; set; }


    }
}