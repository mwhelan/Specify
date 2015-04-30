using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using ContosoUniversity.Infrastructure.DAL.Repositories;
using ContosoUniversity.Models;
using ContosoUniversity.ViewModels;

namespace ContosoUniversity.Controllers
{
    public class InstructorController : Controller
    {
        private readonly IInstructorRepository _instructorRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly IDepartmentRepository _departmentRepository;

        public InstructorController(IInstructorRepository instructorRepository,
            ICourseRepository courseRepository, IDepartmentRepository departmentRepository)
        {
            _instructorRepository = instructorRepository;
            _courseRepository = courseRepository;
            _departmentRepository = departmentRepository;
        }

        // GET: /Instructor/
        public ActionResult Index(int? id, int? courseID)
        {
            var viewModel = new InstructorIndexDataViewModel();

            viewModel.Instructors = _instructorRepository.Get()
                .OrderBy(i => i.LastName);

            if (id != null)
            {
                ViewBag.InstructorID = id.Value;
                viewModel.Courses = viewModel
                    .Instructors
                    .Single(i => i.Id == id.Value)
                    .Courses;
            }

            if (courseID != null)
            {
                ViewBag.CourseID = courseID.Value;
                var selectedCourse = viewModel.Courses.Single(x => x.Id == courseID);
                viewModel.Enrollments = selectedCourse.Enrollments;
            }

            return View(viewModel);
        }

        // GET: /Instructor/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Instructor instructor = _instructorRepository.FindById(id.Value);
            if (instructor == null)
            {
                return HttpNotFound();
            }
            return View(instructor);
        }

        public ActionResult Create()
        {
            var instructor = new Instructor();
            instructor.Courses = new List<Course>();
            PopulateAssignedCourseData(instructor);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LastName,FirstMidName,HireDate,OfficeAssignment")]Instructor instructor, string[] selectedCourses)
        {
            if (selectedCourses != null)
            {
                instructor.Courses = new List<Course>();
                foreach (var course in selectedCourses)
                {
                    var courseToAdd = _courseRepository.FindById(int.Parse(course));
                    instructor.Courses.Add(courseToAdd);
                }
            }
            if (ModelState.IsValid)
            {
                _instructorRepository.Update(instructor);
                return RedirectToAction("Index");
            }
            PopulateAssignedCourseData(instructor);
            return View(instructor);
        }

        // GET: /Instructor/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Instructor instructor = _instructorRepository.FindById(id.Value);
            PopulateAssignedCourseData(instructor);
            if (instructor == null)
            {
                return HttpNotFound();
            }
            return View(instructor);
        }

        private void PopulateAssignedCourseData(Instructor instructor)
        {
            var allCourses = _courseRepository.Get();
            var instructorCourses = new HashSet<int>(instructor.Courses.Select(c => c.Id));
            var viewModel = new List<AssignedCourseDataViewModel>();
            foreach (var course in allCourses)
            {
                viewModel.Add(new AssignedCourseDataViewModel
                {
                    CourseID = course.Id,
                    Title = course.Title,
                    Assigned = instructorCourses.Contains(course.Id)
                });
            }
            ViewBag.Courses = viewModel;
        }

        // POST: /Instructor/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? id, string[] selectedCourses)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var instructorToUpdate = _instructorRepository.FindById(id.Value);

            if (TryUpdateModel(instructorToUpdate, "",
               new string[] { "LastName", "FirstMidName", "HireDate", "OfficeAssignment" }))
            {
                try
                {
                    if (String.IsNullOrWhiteSpace(instructorToUpdate.OfficeAssignment.Location))
                    {
                        instructorToUpdate.OfficeAssignment = null;
                    }

                    UpdateInstructorCourses(selectedCourses, instructorToUpdate);

                    _instructorRepository.Update(instructorToUpdate);

                    return RedirectToAction("Index");
                }
                catch (Exception /* dex */)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            PopulateAssignedCourseData(instructorToUpdate);
            return View(instructorToUpdate);
        }
        private void UpdateInstructorCourses(string[] selectedCourses, Instructor instructorToUpdate)
        {
            if (selectedCourses == null)
            {
                instructorToUpdate.Courses = new List<Course>();
                return;
            }

            var selectedCoursesHS = new HashSet<string>(selectedCourses);
            var instructorCourses = new HashSet<int>
                (instructorToUpdate.Courses.Select(c => c.Id));
            foreach (var course in _courseRepository.Get())
            {
                if (selectedCoursesHS.Contains(course.Id.ToString()))
                {
                    if (!instructorCourses.Contains(course.Id))
                    {
                        instructorToUpdate.Courses.Add(course);
                    }
                }
                else
                {
                    if (instructorCourses.Contains(course.Id))
                    {
                        instructorToUpdate.Courses.Remove(course);
                    }
                }
            }
        }

        // GET: /Instructor/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Instructor instructor = _instructorRepository.FindById(id.Value);
            if (instructor == null)
            {
                return HttpNotFound();
            }
            return View(instructor);
        }

        // POST: /Instructor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Instructor instructor = _instructorRepository.FindById(id);

            instructor.OfficeAssignment = null;
            _instructorRepository.Delete(id);

            var department = _departmentRepository.Get().SingleOrDefault(d => d.InstructorId == id);
            if (department != null)
            {
                department.Instructor = null;
                _departmentRepository.Update(department);
            }

            return RedirectToAction("Index");
        }
    }
}
