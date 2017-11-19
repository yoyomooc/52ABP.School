using System;
using System.Linq;
using LTM.School.Application.enumsType;
using LTM.School.Core.Models;

namespace LTM.School.EntityFramework.Data
{
    public class DbInitializer
    {
        public static void Initialize(SchoolDbContext context)
        {
            context.Database.EnsureCreated();

            // 检查是否有学生信息
            if (context.Students.Any())
            {
                return;   //返回，不执行。
            }

            var students = new Student[]
            {
            new Student{RealName = "龙傲天",EnrollmentDate=DateTime.Parse("2005-09-01")},
            new Student{RealName = "王尼玛",EnrollmentDate=DateTime.Parse("2002-09-01")},
            new Student{RealName = "张全蛋",EnrollmentDate=DateTime.Parse("2003-09-01")},
            new Student{RealName = "叶良辰",EnrollmentDate=DateTime.Parse("2002-09-01")},
            new Student{RealName = "和珅",EnrollmentDate=DateTime.Parse("2002-09-01")},
            new Student{RealName = "纪晓岚",EnrollmentDate=DateTime.Parse("2001-09-01")},
            new Student{RealName = "李逍遥",EnrollmentDate=DateTime.Parse("2003-09-01")},
            new Student{RealName = "王小虎",EnrollmentDate=DateTime.Parse("2005-09-01")}
            };
            foreach (Student s in students)
            {
                context.Students.Add(s);
            }
            context.SaveChanges();

            var courses = new Course[]
            {
            new Course{CourseId= 1050,Title="数学",Credits=3},
            new Course{CourseId=4022,Title="政治",Credits=3},
            new Course{CourseId=4041,Title="物理",Credits=3},
            new Course{CourseId=1045,Title="化学",Credits=4},
            new Course{CourseId=3141,Title="生物",Credits=4},
            new Course{CourseId=2021,Title="英语",Credits=3},
            new Course{CourseId=2042,Title="历史",Credits=4}
            };
            foreach (Course c in courses)
            {
                context.Courses.Add(c);
            }
            context.SaveChanges();

            var enrollments = new Enrollment[]
            {
            new Enrollment{StudentId= 1,CourseId=1050,Grade=CourseGrade.A},
            new Enrollment{StudentId=1,CourseId=4022,Grade=CourseGrade.C},
            new Enrollment{StudentId=1,CourseId=4041,Grade=CourseGrade.B},
            new Enrollment{StudentId=2,CourseId=1045,Grade=CourseGrade.B},
            new Enrollment{StudentId=2,CourseId=3141,Grade=CourseGrade.F},
            new Enrollment{StudentId=2,CourseId=2021,Grade=CourseGrade.F},
            new Enrollment{StudentId=3,CourseId=1050},
            new Enrollment{StudentId=4,CourseId=1050},
            new Enrollment{StudentId=4,CourseId=4022,Grade=CourseGrade.F},
            new Enrollment{StudentId=5,CourseId=4041,Grade=CourseGrade.C},
            new Enrollment{StudentId=6,CourseId=1045},
            new Enrollment{StudentId=7,CourseId=3141,Grade=CourseGrade.A},
            };
            foreach (Enrollment e in enrollments)
            {
                context.Enrollments.Add(e);
            }
            context.SaveChanges();
        }
    }
}