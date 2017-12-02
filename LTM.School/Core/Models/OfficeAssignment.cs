using System.ComponentModel.DataAnnotations;

namespace LTM.School.Core.Models
{

    /// <summary>
    /// 分配办公室
    /// </summary>
    public class OfficeAssignment
    {

        /// <summary>
        /// 教师Id
        /// </summary>
        [Key]
        public int InstrctorId { get; set; }
        
        /// <summary>
        /// 办公室的位置
        /// </summary>
       [StringLength(50)]
        [Display(Name = "Office Location")]
        public string Location{get; set;}
        /// <summary>
        /// 教师的导航属性
        /// </summary>
        public Instructor Instructor { get; set; }

    }
}