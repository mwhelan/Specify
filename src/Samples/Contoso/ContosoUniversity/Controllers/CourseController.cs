using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using ContosoUniversity.Domain.Model;
using ContosoUniversity.Infrastructure.DAL.Repositories;
using ContosoUniversity.Infrastructure.Mapping;
using ContosoUniversity.ViewModels;

namespace ContosoUniversity.Controllers
{
    public class CourseController : Controller
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;

        public CourseController(ICourseRepository courseRepository, IDepartmentRepository departmentRepository,
            IMapper mapper)
        {
            _courseRepository = courseRepository;
            _departmentRepository = departmentRepository;
            _mapper = mapper;
        }

        // GET: /Course/
        public ActionResult Index(int? selectedDepartment)
        {
            PopulateDepartmentsDropDownList(selectedDepartment);
            int departmentID = selectedDepartment.GetValueOrDefault();

            var courses = _courseRepository.Get()
                .Where(c => !selectedDepartment.HasValue || c.DepartmentId == departmentID)
                .OrderBy(d => d.Id);

            return View(_mapper.Map<IEnumerable<Course>,IEnumerable<CourseViewModel>>(courses));
        }

        // GET: /Course/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = _courseRepository.FindById(id.Value);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(_mapper.Map<Course,CourseViewModel>(course));
        }

        public ActionResult Create()
        {
            PopulateDepartmentsDropDownList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Credits,DepartmentID")]CourseViewModel course)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var department = _departmentRepository.FindById(course.DepartmentId);
                    var model = new Course();
                    model.Credits = course.Credits;
                    model.Department = department;
                    model.Title = course.Title;

                    _courseRepository.Create(model);
                    return RedirectToAction("Index");
                }
            }
            catch (Exception /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.)
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }
            PopulateDepartmentsDropDownList(course.DepartmentId);
            return View(course);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = _courseRepository.FindById(id.Value);
            if (course == null)
            {
                return HttpNotFound();
            }
            PopulateDepartmentsDropDownList(course.DepartmentId);
            return View(_mapper.Map<Course,CourseViewModel>(course));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Credits,DepartmentID")]CourseViewModel course)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var department = _departmentRepository.FindById(course.DepartmentId);
                    var model = _courseRepository.FindById(course.Id);
                    model.Id = course.Id;
                    model.Credits = course.Credits;
                    model.Department = department;
                    model.Title = course.Title;

                    _courseRepository.Update(model);
                    return RedirectToAction("Index");
                }
            }
            catch (Exception /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.)
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }
            PopulateDepartmentsDropDownList(course.DepartmentId);
            return View(course);
        }

        private void PopulateDepartmentsDropDownList(object selectedDepartment = null)
        {
            var departmentsQuery = from d in _departmentRepository.Get()
                                   orderby d.Name
                                   select d;
            ViewBag.DepartmentID = new SelectList(departmentsQuery, "ID", "Name", selectedDepartment);
        } 

        // GET: /Course/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = _courseRepository.FindById(id.Value);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(_mapper.Map<Course, CourseViewModel>(course));
        }

        // POST: /Course/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _courseRepository.Delete(id);
            return RedirectToAction("Index");
        }

        public ActionResult UpdateCourseCredits()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UpdateCourseCredits(int? multiplier)
        {
            if (multiplier != null)
            {
                //ViewBag.RowsAffected = db.Database.ExecuteSqlCommand("UPDATE Course SET Credits = Credits * {0}", multiplier);
            }
            return View();
        }
    }
}
