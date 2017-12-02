using System;
using System.Collections.Generic;

namespace LTM.School.Core.Models
{

    /// <summary>
    /// 教师
    /// </summary>
    public class Instructor
    {
        public int Id { get; set; }

        public string RealName { get; set; }

        public  DateTime HireDate { get; set; }


        public ICollection<CourseAssignment> CourseAssignments { get; set; }


        public OfficeAssignment OfficeAssignment { get; set; }


}
}