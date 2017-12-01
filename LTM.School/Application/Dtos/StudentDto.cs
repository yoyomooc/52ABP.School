using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using LTM.School.Core.Models;

namespace LTM.School.Application.Dtos
{
    public class StudentDto
    {

        [RequiredAttribute]
        [MaxLength(30, ErrorMessage = "姓名长度不能超过30个字符。")]
        [DisplayName("学生姓名")]
        public string RealName { get; set; }

        [DisplayName("注册时间")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime EnrollmentDate { get; set; }



    }
}