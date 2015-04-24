using System;
using System.Data;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using ContosoUniversity.DAL;
using ContosoUniversity.Models;

namespace ContosoUniversity.Controllers
{
    public class DepartmentController : Controller
    {
        // GET: /Department/
        public async Task<ActionResult> Index()
        {
            var departments = Database.Departments;
            return View(departments);
        }

        // GET: /Department/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Department department = Database.Departments.SingleOrDefault(x => x.Id == id.Value);
            
            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);
        }

        // GET: /Department/Create
        public ActionResult Create()
        {
            ViewBag.InstructorID = new SelectList(Database.Instructors, "ID", "FullName");
            return View();
        }

        // POST: /Department/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "DepartmentID,Name,Budget,StartDate,InstructorID")] Department department)
        {
            if (ModelState.IsValid)
            {
                Database.Departments.Add(department);
                return RedirectToAction("Index");
            }

            ViewBag.InstructorID = new SelectList(Database.Instructors, "ID", "FullName", department.InstructorID);
            return View(department);
        }

        // GET: /Department/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = Database.Departments.SingleOrDefault(x => x.Id == id.Value);
            if (department == null)
            {
                return HttpNotFound();
            }
            ViewBag.InstructorID = new SelectList(Database.Instructors, "ID", "FullName", department.InstructorID);
            return View(department);
        }

        // POST: /Department/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(
           [Bind(Include = "DepartmentID, Name, Budget, StartDate, RowVersion, InstructorID")] 
       Department department)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ValidateOneAdministratorAssignmentPerInstructor(department);
                }
                if (ModelState.IsValid)
                {
                    Database.Departments.RemoveAll(x => x.Id == department.Id);
                    Database.Departments.Add(department);
                    return RedirectToAction("Index");
                }
            }
            catch (Exception /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError(string.Empty, "Unable to save changes. Try again, and if the problem persists contact your system administrator.");
            }

            ViewBag.InstructorID = new SelectList(Database.Instructors, "ID", "FullName", department.InstructorID);
            return View(department);
        }

        private void ValidateOneAdministratorAssignmentPerInstructor(Department department)
        {
            if (department.InstructorID != null)
            {
                Department duplicateDepartment = Database.Departments
                    .FirstOrDefault(d => d.InstructorID == department.InstructorID);
                if (duplicateDepartment != null && duplicateDepartment.Id != department.Id)
                {
                    string errorMessage = String.Format(
                        "Instructor {0} {1} is already administrator of the {2} department.",
                        duplicateDepartment.Instructor.FirstMidName,
                        duplicateDepartment.Instructor.LastName,
                        duplicateDepartment.Name);
                    ModelState.AddModelError(string.Empty, errorMessage);
                }
            }
        }

        // GET: /Department/Delete/5
        public async Task<ActionResult> Delete(int? id, bool? concurrencyError)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = Database.Departments.SingleOrDefault(x => x.Id == id.Value);
            if (department == null)
            {
                if (concurrencyError == true)
                {
                    return RedirectToAction("Index");
                }
                return HttpNotFound();
            }

            if (concurrencyError.GetValueOrDefault())
            {
                if (department == null)
                {
                    ViewBag.ConcurrencyErrorMessage = "The record you attempted to delete "
                        + "was deleted by another user after you got the original values. "
                        + "Click the Back to List hyperlink.";
                }
                else
                {
                    ViewBag.ConcurrencyErrorMessage = "The record you attempted to delete "
                        + "was modified by another user after you got the original values. "
                        + "The delete operation was canceled and the current values in the "
                        + "database have been displayed. If you still want to delete this "
                        + "record, click the Delete button again. Otherwise "
                        + "click the Back to List hyperlink.";
                }
            }

            return View(department);
        }

        // POST: /Department/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(Department department)
        {
            try
            {
                Database.Departments.Remove(department);
                return RedirectToAction("Index");
            }
            catch (DataException /* dex */)
            {
                //Log the error (uncomment dex variable name after DataException and add a line here to write a log.
                ModelState.AddModelError(string.Empty, "Unable to delete. Try again, and if the problem persists contact your system administrator.");
                return View(department);
            }
        }
    }
}
