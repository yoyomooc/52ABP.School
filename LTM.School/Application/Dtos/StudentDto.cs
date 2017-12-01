using System;
using System.Collections.Generic;
using System.ComponentModel;
using LTM.School.Core.Models;

namespace LTM.School.Application.Dtos
{
    public class StudentDto
    {

        [DisplayName("姓名")]
        public string RealName { get; set; }
        [DisplayName("注册时间")]
        public DateTime EnrollmentDate { get; set; }

     

    }
}