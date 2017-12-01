using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LTM.School.Core.Models
{
    /// <summary>
    ///     学生
    /// </summary>
    public class Student
    {
        public int Id { get; set; }



        [Required]
        [StringLength(30,ErrorMessage = "姓名长度不能超过30个字符。")]
        [DisplayName("学生姓名")]
        public string RealName { get; set; }

        [DisplayName("注册时间")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}",ApplyFormatInEditMode = true)]
        public DateTime EnrollmentDate { get; set; }

        [DisplayName("登记信息")]
        public ICollection<Enrollment> Enrollments { get; set; }

        [MaxLength(200)]
        public string Secret { get; set; }
    }
}