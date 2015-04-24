using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ContosoUniversity.DAL;
using ContosoUniversity.ViewModels;

namespace ContosoUniversity.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            IEnumerable<EnrollmentDateGroup> data =
                Database.Students.GroupBy(student => student.EnrollmentDate)
                    .Select(dateGroup => new EnrollmentDateGroup()
                    {
                        EnrollmentDate = dateGroup.Key,
                        StudentCount = dateGroup.Count()
                    })
                    .ToList();

            return View(data);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}