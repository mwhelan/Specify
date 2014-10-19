using System;
using System.Web.Mvc;

namespace ContosoUniversity.AcceptanceTests.Infrastructure
{
    public class ControllerScenario<TController, TActionResult, TModel> //: ISystemUnderTest<TController>
        where TController : Controller
        where TActionResult : ActionResult
    {
        public TController SUT { get; set; }
        public TActionResult ActionResult { get; set; }
        public Exception Exception { get; set; }
        public TModel Model { get; set; }

        public virtual void ExecuteAction(Func<TController, TActionResult> func)
        {
            //SUT.Url = new UrlHelper(new RequestContext(FakeHttpContext.Root(), new RouteData()), RouteTable.Routes);

            try
            {
                ActionResult = func(SUT);
            }
            catch (Exception ex)
            {
                Exception = ex;
                if (Exception is NotImplementedException) throw ex;
            }
        }
    }
}
