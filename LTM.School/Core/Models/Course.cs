using System.Collections.Generic;
using System.ComponentModel;
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

      [Display(Name = "课程编号")]
        public int CourseId { get; set; }

        [DisplayName("课程名称")]
        [StringLength(50,MinimumLength = 2)]
        public string Title { get; set; }
        /// <summary>
        /// 学分
        /// </summary>
        [Range(0,5)]
   [DisplayName("学分")]
        public int Credits { get; set; }
        /// <summary>
        /// 课程成绩
        /// </summary>
        [DisplayName("课程成绩")]
        public CourseGrade Grade { get; set; }

       
        /// <summary>
        /// 部门id
        /// </summary>
        public int DepartmentId { get; set; }

        public Department Department { get; set; }
        public ICollection<CourseAssignment> CourseAssignments { get; set; }

        public ICollection<Enrollment> Enrollments { get; set; }


    }
}