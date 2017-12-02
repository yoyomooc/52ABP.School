using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LTM.School.Core.Models
{
    /// <summary>
    /// 部门
    /// </summary>
    public class Department
    {
        public int Id { get; set; }

        [StringLength(50,MinimumLength = 2)]
        public string Name { get; set; }
        
        /// <summary>
        /// 预算
        /// </summary>
        [Column(TypeName = "money")]
        [DataType(DataType.Currency)]
        public decimal Budget { get; set; }

        /// <summary>
        /// 开课时间
        /// </summary>
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}",ApplyFormatInEditMode = true)]
        [Display(Name = "开课时间")]
        public DateTime StarDate { get; set; }


        public int? InstructorId { get; set; }

        /// <summary>
        /// 办公室主任
        /// </summary>
        public  Instructor Administrator { get; set; }

        /// <summary>
        /// 课程
        /// </summary>
        public ICollection<Course> Courses { get; set; }



    }
}