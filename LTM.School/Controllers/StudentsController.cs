using System.Linq;
using System.Threading.Tasks;
using LTM.School.Application.Dtos;
using LTM.School.Common;
using LTM.School.Core.Models;
using LTM.School.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LTM.School.Controllers
{
    public class StudentsController : Controller
    {
        private readonly SchoolDbContext _context;

        public StudentsController(SchoolDbContext context)
        {
            _context = context;
        }

        // GET: Students
        public async Task<IActionResult> Index(string sortOrder, string searchStudent, int? page, string currentStudent)
        {
            #region   搜索和排序

            //姓名的排序参数
            ViewData["Name_Sort_Parm"] = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            //时间的排序参数
            ViewData["Date_Sort_Parm"] = sortOrder == "Date" ? "date_desc" : "Date";
            //搜索的关键字
            ViewData["SearchStudent"] = searchStudent;

            #endregion


            ViewData["CurrentSort"] = sortOrder;


            if (searchStudent != null)
                page = 1;
            else
                searchStudent = currentStudent;

            #region 搜索和排序功能

            var students = from student in _context.Students select student;

            if (!string.IsNullOrWhiteSpace(searchStudent))
                students = students.Where(a => a.RealName.Contains(searchStudent));


            switch (sortOrder)
            {
                case "name_desc":
                    students = students.OrderByDescending(a => a.RealName);
                    break;

                case "Date":
                    students = students.OrderBy(a => a.EnrollmentDate);
                    break;

                case "date_desc":
                    students = students.OrderByDescending(a => a.EnrollmentDate);
                    break;

                default:
                    students = students.OrderBy(a => a.RealName);
                    break;
            }

            #endregion

            var pageSize = 5;


            var entities = students.AsNoTracking();

            var dtos = await PaginatedList<Student>.CreatepagingAsync(entities, page ?? 1, pageSize);

            return View(dtos);
        }


        // GET: Students/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var student = await _context.Students.Include(a => a.Enrollments).ThenInclude(e => e.Course).AsNoTracking()
                .SingleOrDefaultAsync(m => m.Id == id);

            //FirstOrDefaultAsync
            if (student == null)
                return NotFound();

            return View(student);
        }

        // GET: Students/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StudentDto dto)
        {
            //CSRF参考资料：   http://www.freebuf.com/articles/web/55965.html


            try
            {
                if (ModelState.IsValid)
                {
                    var entity = new Student
                    {
                        RealName = dto.RealName,
                        EnrollmentDate = dto.EnrollmentDate
                    };


                    _context.Add(entity);
                    await _context.SaveChangesAsync();

                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException ex)
            {
                ModelState.AddModelError("", "无法进行数据的保存，请仔细检查你的数据，是否异常。");
            }

            return View(dto);
        }

        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var student = await _context.Students.SingleOrDefaultAsync(m => m.Id == id);
            if (student == null)
                return NotFound();
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,RealName,EnrollmentDate")] Student student)
        {
            if (id != student.Id)
                return NotFound();

            var entity = await _context.Students.SingleOrDefaultAsync(a => a.Id == id);

            if (await TryUpdateModelAsync(entity, "", s => s.RealName, s => s.EnrollmentDate))
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException e)
                {
                    ModelState.AddModelError("", "无法进行数据的保存，请仔细检查你的数据，是否异常。");
                }


            return View(student);
        }

        // GET: Students/Delete/5
        public async Task<IActionResult> Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
                return NotFound();

            var student = await _context.Students.AsNoTracking()
                .SingleOrDefaultAsync(m => m.Id == id);
            if (student == null)
                return NotFound();

            if (saveChangesError.GetValueOrDefault())
                ViewBag.SaveError = "删除失败。请再次尝试，如果尝试失败，请联系系统管理员。";

            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var student = await _context.Students.AsNoTracking().SingleOrDefaultAsync(m => m.Id == id);
            if (student == null)
                return RedirectToAction(nameof(Index));
            try
            {
                _context.Students.Remove(student);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException e)
            {
                return RedirectToAction(nameof(Delete), new {id, saveChangesError = true});
            }
        }

        private bool StudentExists(int id)
        {
            return _context.Students.Any(e => e.Id == id);
        }
    }
}