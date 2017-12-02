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
            //    context.Database.EnsureCreated();

            // 检查是否有学生信息
            if (context.Students.Any())
                return; //返回，不执行。

            #region 添加种子学生信息

            var students = new[]
            {
                new Student {RealName = "龙傲天", EnrollmentDate = DateTime.Parse("2005-09-01")},
                new Student {RealName = "王尼玛", EnrollmentDate = DateTime.Parse("2002-09-01")},
                new Student {RealName = "张全蛋", EnrollmentDate = DateTime.Parse("2003-09-01")},
                new Student {RealName = "叶良辰", EnrollmentDate = DateTime.Parse("2002-09-01")},
                new Student {RealName = "和珅", EnrollmentDate = DateTime.Parse("2002-09-01")},
                new Student {RealName = "纪晓岚", EnrollmentDate = DateTime.Parse("2001-09-01")},
                new Student {RealName = "李逍遥", EnrollmentDate = DateTime.Parse("2003-09-01")},
                new Student {RealName = "王小虎", EnrollmentDate = DateTime.Parse("2005-09-01")}
            };
            foreach (var s in students)
                context.Students.Add(s);
            context.SaveChanges();

            #endregion


            #region 添加种子老师信息

            var instructors = new[]
            {
                new Instructor
                {
                    RealName = "孔子",
                    HireDate = DateTime.Parse("1995-03-11")
                },
                new Instructor
                {
                    RealName = "墨子",
                    HireDate = DateTime.Parse("2003-03-11")
                },
                new Instructor
                {
                    RealName = "荀子",
                    HireDate = DateTime.Parse("1990-03-11")
                },
                new Instructor
                {
                    RealName = "鬼谷子",
                    HireDate = DateTime.Parse("1985-03-11")
                },
                new Instructor
                {
                    RealName = "孟子",
                    HireDate = DateTime.Parse("2003-03-11")
                },
                new Instructor
                {
                    RealName = "朱熹",
                    HireDate = DateTime.Parse("2003-03-11")
                }
            };

            foreach (var i in instructors)
                context.Instructors.Add(i);
            context.SaveChanges();

            #endregion


            #region 添加部门的种子的数据

            var departments = new[]
            {
                new Department
                {
                    Name = "论语",
                    Budget = 350000,
                    StartDate = DateTime.Parse("2017-09-01"),
                    InstructorId = instructors.Single(i => i.RealName == "孟子").Id
                },
                new Department
                {
                    Name = "兵法",
                    Budget = 100000,
                    StartDate = DateTime.Parse("2017-09-01"),
                    InstructorId = instructors.Single(i => i.RealName == "鬼谷子").Id
                },
                new Department
                {
                    Name = "文言文",
                    Budget = 350000,
                    StartDate = DateTime.Parse("2017-09-01"),
                    InstructorId = instructors.Single(i => i.RealName == "朱熹").Id
                },
                new Department
                {
                    Name = "世界和平",
                    Budget = 100000,
                    StartDate = DateTime.Parse("2017-09-01"),
                    InstructorId = instructors.Single(i => i.RealName == "墨子").Id
                }
            };

            foreach (var d in departments)
                context.Departments.Add(d);
            context.SaveChanges();

            #endregion


            var courses = new[]
            {
                new Course
                {
                    CourseId = 1050,
                    Title = "数学",
                    Credits = 3,
                    DepartmentId = departments.Single(s => s.Name == "兵法").Id
                },
                new Course
                {
                    CourseId = 4022,
                    Title = "政治",
                    Credits = 3,
                    DepartmentId = departments.Single(s => s.Name == "文言文").Id
                },
                new Course
                {
                    CourseId = 4041,
                    Title = "物理",
                    Credits = 3,
                    DepartmentId = departments.Single(s => s.Name == "兵法").Id
                },
                new Course
                {
                    CourseId = 1045,
                    Title = "化学",
                    Credits = 4,
                    DepartmentId = departments.Single(s => s.Name == "世界和平").Id
                },
                new Course
                {
                    CourseId = 3141,
                    Title = "生物",
                    Credits = 4,
                    DepartmentId = departments.Single(s => s.Name == "论语").Id
                },
                new Course
                {
                    CourseId = 2021,
                    Title = "英语",
                    Credits = 3,
                    DepartmentId = departments.Single(s => s.Name == "论语").Id
                },
                new Course
                {
                    CourseId = 2042,
                    Title = "历史",
                    Credits = 4,
                    DepartmentId = departments.Single(s => s.Name == "文言文").Id
                }
            };


            foreach (var c in courses)
                context.Courses.Add(c);
            context.SaveChanges();


            #region 办公室分配的种子数据

            var officeAssignments = new[]
            {
                new OfficeAssignment
                {
                    InstrctorId = instructors.Single(i => i.RealName == "孟子").Id,
                    Location = "逸夫楼 17"
                },
                new OfficeAssignment
                {
                    InstrctorId = instructors.Single(i => i.RealName == "朱熹").Id,
                    Location = "青霞路 27"
                },
                new OfficeAssignment
                {
                    InstrctorId = instructors.Single(i => i.RealName == "墨子").Id,
                    Location = "天府楼 304"
                }
            };

            foreach (var o in officeAssignments)
                context.OfficeAssignments.Add(o);
            context.SaveChanges();

            #endregion

            #region 课程老师的种子数据

            var courseInstructors = new[]
            {
                new CourseAssignment
                {
                    CourseId = courses.Single(c => c.Title == "数学").CourseId,
                    InstructorId = instructors.Single(i => i.RealName == "鬼谷子").Id
                },
                new CourseAssignment
                {
                    CourseId = courses.Single(c => c.Title == "数学").CourseId,
                    InstructorId = instructors.Single(i => i.RealName == "墨子").Id
                },
                new CourseAssignment
                {
                    CourseId = courses.Single(c => c.Title == "政治").CourseId,
                    InstructorId = instructors.Single(i => i.RealName == "朱熹").Id
                },
                new CourseAssignment
                {
                    CourseId = courses.Single(c => c.Title == "化学").CourseId,
                    InstructorId = instructors.Single(i => i.RealName == "墨子").Id
                },
                new CourseAssignment
                {
                    CourseId = courses.Single(c => c.Title == "生物").CourseId,
                    InstructorId = instructors.Single(i => i.RealName == "孟子").Id
                },
                new CourseAssignment
                {
                    CourseId = courses.Single(c => c.Title == "英语").CourseId,
                    InstructorId = instructors.Single(i => i.RealName == "孟子").Id
                },
                new CourseAssignment
                {
                    CourseId = courses.Single(c => c.Title == "物理").CourseId,
                    InstructorId = instructors.Single(i => i.RealName == "鬼谷子").Id
                },
                new CourseAssignment
                {
                    CourseId = courses.Single(c => c.Title == "历史").CourseId,
                    InstructorId = instructors.Single(i => i.RealName == "朱熹").Id
                },
                new CourseAssignment
                {
                    CourseId = courses.Single(c => c.Title == "Literature").CourseId,
                    InstructorId = instructors.Single(i => i.RealName == "Abercrombie").Id
                }
            };

            foreach (var ci in courseInstructors)
                context.CourseAssignments.Add(ci);
            context.SaveChanges();

            #endregion


            var enrollments = new[]
            {
                new Enrollment
                {
                    StudentId = students.Single(s => s.RealName == "龙傲天").Id,
                    CourseId = courses.Single(c => c.Title == "数学").CourseId,
                    Grade = CourseGrade.A
                },
                new Enrollment
                {
                    StudentId = students.Single(s => s.RealName == "龙傲天").Id,
                    CourseId = courses.Single(c => c.Title == "政治").CourseId,
                    Grade = CourseGrade.C
                },
                new Enrollment
                {
                    StudentId = students.Single(s => s.RealName == "龙傲天").Id,
                    CourseId = courses.Single(c => c.Title == "物理").CourseId,
                    Grade = CourseGrade.D
                },
                new Enrollment
                {
                    StudentId = students.Single(s => s.RealName == "王尼玛").Id,
                    CourseId = courses.Single(c => c.Title == "物理").CourseId,
                    Grade = CourseGrade.F
                },
                new Enrollment
                {
                    StudentId = students.Single(s => s.RealName == "王尼玛").Id,
                    CourseId = courses.Single(c => c.Title == "化学").CourseId
                },
                new Enrollment
                {
                    StudentId = students.Single(s => s.RealName == "王尼玛").Id,
                    CourseId = courses.Single(c => c.Title == "生物").CourseId
                },
                new Enrollment
                {
                    StudentId = students.Single(s => s.RealName == "叶良辰").Id,
                    CourseId = courses.Single(c => c.Title == "英语").CourseId,
                    Grade = CourseGrade.A
                }, new Enrollment
                {
                    StudentId = students.Single(s => s.RealName == "叶良辰").Id,
                    CourseId = courses.Single(c => c.Title == "历史").CourseId,
                    Grade = CourseGrade.D
                },
                new Enrollment
                {
                    StudentId = students.Single(s => s.RealName == "张全蛋").Id,
                    CourseId = courses.Single(c => c.Title == "英语").CourseId,
                    Grade = CourseGrade.B
                },
                new Enrollment
                {
                    StudentId = students.Single(s => s.RealName == "张全蛋").Id,
                    CourseId = courses.Single(c => c.Title == "数学").CourseId,
                    Grade = CourseGrade.A
                },
                new Enrollment
                {
                    StudentId = students.Single(s => s.RealName == "纪晓岚").Id,
                    CourseId = courses.Single(c => c.Title == "英语").CourseId
                },
                new Enrollment
                {
                    StudentId = students.Single(s => s.RealName == "王小虎").Id,
                    CourseId = courses.Single(c => c.Title == "生物").CourseId
                },
                new Enrollment
                {
                    StudentId = students.Single(s => s.RealName == "和珅").Id,
                    CourseId = courses.Single(c => c.Title == "物理").CourseId,
                    Grade = CourseGrade.A
                },
                new Enrollment
                {
                    StudentId = students.Single(s => s.RealName == "和珅").Id,
                    CourseId = courses.Single(c => c.Title == "英语").CourseId
                }
            };
            foreach (var e in enrollments)
                context.Enrollments.Add(e);
            context.SaveChanges();
        }
    }
}