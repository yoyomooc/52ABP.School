namespace LTM.School.Core.Models
{
    /// <summary>
    /// 课程分配
    /// </summary>
    public class CourseAssignment
    {
        /// <summary>
        /// 教师id
        /// </summary>
        public int InstructorId { get; set; }
        

        public int CourseId { get; set; }

        public Course Course { get; set; }
        public Instructor Instructor { get; set; }

    }
}