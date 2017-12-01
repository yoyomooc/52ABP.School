using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LTM.School.ViewModels
{
    public class EnrollmentDateGroup
    {
        [DisplayName("学生总数")]
        public int StudentCount { get; set; }

        [DisplayName("学生注册日期")]
        [DataType(DataType.Date)]
        public DateTime? EnrollmenDate { get; set; }


    }
}