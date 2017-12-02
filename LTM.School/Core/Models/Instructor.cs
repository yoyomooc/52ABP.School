using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace LTM.School.Core.Models
{

    /// <summary>
    /// 教师
    /// </summary>
    public class Instructor
    {
        public int Id { get; set; }

        [DisplayName("教师信息")]
        public string RealName { get; set; }
        [DisplayName("入职时间")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime HireDate { get; set; }

        

        public ICollection<CourseAssignment> CourseAssignments { get; set; }


        public OfficeAssignment OfficeAssignment { get; set; }


}
}