using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace LTM.School.Core.Models
{
    /// <summary>
    ///     学生
    /// </summary>
    public class Student
    {
        public int Id { get; set; }


        [DisplayName("学生姓名")]
        public string RealName { get; set; }

        [DisplayName("注册时间")]
        public DateTime EnrollmentDate { get; set; }

        [DisplayName("登记信息")]
        public ICollection<Enrollment> Enrollments { get; set; }


        public string Secret { get; set; }
    }
}