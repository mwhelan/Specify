using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Specify;
using Subtext.TestLibrary;
using TestStack.FluentMVCTesting;
using Xania.AspNet.Simulator;

namespace ContosoUniversity.SubcutaneousTests.Infrastructure
{
    public class MvcControllerDriver : IDisposable
    {
        protected internal IContainer Container { get; set; }
        private readonly RouteCollection _routes;
        private readonly HttpSimulator _httpRequest;

        public MvcControllerDriver(RouteCollection routes)
        {
            _routes = routes;
            _httpRequest = new HttpSimulator().SimulateRequest();
        }

        public ControllerResultTest<TController> ExecuteActionFor<TController>(
            Expression<Func<TController, ActionResult>> action) where TController : Controller
        {
            var controller = ConstructController<TController>();
            return controller.WithCallTo(action);
        }

        public ControllerResultTest<TController> ExecuteActionFor<TController>(
            Expression<Func<TController, Task<ActionResult>>> action)
            where TController : Controller
        {
            var controller = ConstructController<TController>();
            return controller.WithCallTo(action);
        }

        public ControllerActionResult Execute<TController>(Expression<Func<TController, object>> actionExpression)
            where TController : Controller
        {
            var controller = ConstructController<TController>();
            return controller.Execute(actionExpression);
        }

        private TController ConstructController<TController>() where TController : Controller
        {
            var controller = Container.Get<TController>();
            controller.ControllerContext = new ControllerContext(new HttpContextWrapper(HttpContext.Current),
                new RouteData(), controller);
            controller.Url = new UrlHelper(controller.Request.RequestContext, _routes);

            return controller;
        }

        public void Dispose()
        {
            _httpRequest.Dispose();
        }
    }
}
