using System;
using System.Web.Mvc;

namespace ContosoUniversity.AcceptanceTests.Infrastructure
{
    public class ViewControllerScenario<TController, TViewModel>
        : ControllerScenario<TController, ActionResult, TViewModel>
        where TController : Controller
        where TViewModel : class
    {
        public override void ExecuteAction(Func<TController, ActionResult> func)
        {
            base.ExecuteAction(func);
            try
            {
                if (ActionResult is ViewResult)
                {
                    Model = (TViewModel)(ActionResult as ViewResult).Model;
                }
            }
            catch (InvalidCastException) { }
        }
    }
}