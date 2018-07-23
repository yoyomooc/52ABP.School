using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LTM.School.Core.Models;
using LTM.School.EntityFramework;
using LTM.School.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LTM.School.Controllers
{
    public class InstructorsController : Controller
    {
        private readonly SchoolDbContext _context;

        public InstructorsController(SchoolDbContext context)
        {
            _context = context;
        }

        // GET: Instructors
        public async Task<IActionResult> Index(int? id, int? courseId)
        {
            var viewModel = new InstructorIndexData
            {
                Instructors = await _context.Instructors
                    .Include(a => a.OfficeAssignment)
                    .Include(a => a.CourseAssignments)
                    .ThenInclude(a => a.Course)
                    .ThenInclude(a => a.Enrollments)
                    .ThenInclude(a => a.Student)
                    .Include(a => a.CourseAssignments)
                    .ThenInclude(a => a.Course)
                    .ThenInclude(a => a.Department)
                    .AsNoTracking().OrderBy(a => a.RealName).ToListAsync()
            };


            if (id != null)
            {
                ViewData["InstructorId"] = id.Value;
                var instructor = viewModel.Instructors.Single(a => a.Id == id.Value);

                viewModel.Courses = instructor.CourseAssignments.Select(a => a.Course).ToList();
            }

            if (courseId != null)
            {
                ViewData["CourseId"] = courseId.Value;
                viewModel.Enrollments = viewModel.Courses.Single(a => a.CourseId == courseId).Enrollments.ToList();
            }


            return View(viewModel);
        }

        // GET: Instructors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var instructor = await _context.Instructors
                .SingleOrDefaultAsync(m => m.Id == id);
            if (instructor == null)
                return NotFound();

            return View(instructor);
        }

        // GET: Instructors/Create
        public IActionResult Create()
        {
            var instructor = new Instructor {CourseAssignments = new List<CourseAssignment>()};



            PopulateAssignedCourseData(instructor);


            return View();
        }

        // POST: Instructors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RealName,HireDate,OfficeAssignment")] Instructor instructor,string [] selectedCourses)
        {

            if (selectedCourses!=null)
            {
            


                instructor.CourseAssignments=new List<CourseAssignment>();
                foreach (var courseId in selectedCourses)
                {
                    var course = new CourseAssignment
                    {
                        CourseId = Convert.ToInt32(courseId),
                        InstructorId = instructor.Id
                    };
                    instructor.CourseAssignments.Add(course);
                }

            }
            
            if (ModelState.IsValid)
            {
                _context.Add(instructor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            PopulateAssignedCourseData(instructor);

            return View(instructor);
        }

        // GET: Instructors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var instructor = await _context.Instructors
                .Include(a => a.OfficeAssignment)
                .Include(a => a.CourseAssignments)
                .ThenInclude(a => a.Course).AsNoTracking()
                .SingleOrDefaultAsync(m => m.Id == id);
            if (instructor == null)
                return NotFound();

            PopulateAssignedCourseData(instructor);
            return View(instructor);
        }

        // POST: Instructors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int? id,string [] selectedCourses)
        {
            if (id == null)
                return NotFound();

            var instructorToUpdate = await _context.Instructors.Include(a => a.OfficeAssignment)
                .Include(a => a.CourseAssignments).ThenInclude(a => a.Course)
                .SingleOrDefaultAsync(s => s.Id == id);


            if (await TryUpdateModelAsync(instructorToUpdate, "", a => a.RealName, a => a.HireDate,
                a => a.OfficeAssignment))
            {
                if (string.IsNullOrWhiteSpace(instructorToUpdate.OfficeAssignment?.Location))
                    instructorToUpdate.OfficeAssignment = null;

                UpdateInstructorCourses(selectedCourses, instructorToUpdate);

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException e)
                {
                    ModelState.AddModelError("", "无法进行数据的保存，请仔细检查你的数据，是否异常。");
                }

                return RedirectToAction(nameof(Index));
            }

            UpdateInstructorCourses(selectedCourses, instructorToUpdate);
            PopulateAssignedCourseData(instructorToUpdate);

            return View(instructorToUpdate);
        }

        // GET: Instructors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var instructor = await _context.Instructors
                .SingleOrDefaultAsync(m => m.Id == id);
            if (instructor == null)
                return NotFound();

            return View(instructor);
        }

        // POST: Instructors/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var instructor = await _context.Instructors.Include(a=>a.CourseAssignments).SingleOrDefaultAsync(m => m.Id == id);
            var departments = await _context.Departments.Where(a => a.InstructorId == id).ToListAsync();
            departments.ForEach(a=>a.InstructorId=null);


            _context.Instructors.Remove(instructor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        #region 更改老师的授课信息表

        /// <summary>
        ///     更改老师的授课信息表
        /// </summary>
        /// <param name="selecteedCourses">选中的课程信息</param>
        /// <param name="instructorToUpdate">需要修改信息的教师实体</param>
        private void UpdateInstructorCourses(string[] selecteedCourses, Instructor instructorToUpdate)
        {
            if (selecteedCourses == null)
            {
                instructorToUpdate.CourseAssignments = new List<CourseAssignment>();
                return;
            }
            var selectedCoursesHs = new HashSet<string>(selecteedCourses);
            var instructorCourses = new HashSet<int>(instructorToUpdate.CourseAssignments.Select(a => a.CourseId));

            foreach (var course in _context.Courses)

                if (selectedCoursesHs.Contains(course.CourseId.ToString()))
                {
                    if (!instructorCourses.Contains(course.CourseId))
                        instructorToUpdate.CourseAssignments.Add(
                            new CourseAssignment
                            {
                                InstructorId = instructorToUpdate.Id,
                                CourseId = course.CourseId
                            });
                }
                else
                {
                    if (instructorCourses.Contains(course.CourseId))
                    {
                        var courseToRemove =
                            instructorToUpdate.CourseAssignments.SingleOrDefault(a => a.CourseId == course.CourseId);

                        _context.Remove(courseToRemove);
                    }
                }
        }

        #endregion


        #region 填充分配课程表

        /// <summary>
        ///     给老师分配课程
        /// </summary>
        /// <param name="instructor"></param>
        private void PopulateAssignedCourseData(Instructor instructor)
        {
            var couserList = _context.Courses;


            var instructorCourses = new HashSet<int>(instructor.CourseAssignments.Select(a => a.CourseId));

            var viewModel = new List<AssignedCourseData>();

            foreach (var course in couserList)
                viewModel.Add(new AssignedCourseData
                {
                    CourseId = course.CourseId,
                    Title = course.Title,
                    Assigned = instructorCourses.Contains(course.CourseId)
                });

            ViewData["Cousers"] = viewModel;
        }

        #endregion


        private bool InstructorExists(int id)
        {
            return _context.Instructors.Any(e => e.Id == id);
        }
    }
}