namespace LTM.School.ViewModels
{

    /// <summary>
    /// 分配课程数据
    /// </summary>
    public class AssignedCourseData
    {
        /// <summary>
        /// 课程ID
        /// </summary>
        public int CourseId { get; set; }
        /// <summary>
        /// 课程名称
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 是否分配
        /// </summary>
        public bool Assigned { get; set; }
        
    }
}