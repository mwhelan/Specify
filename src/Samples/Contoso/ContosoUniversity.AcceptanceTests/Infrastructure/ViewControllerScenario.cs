using System;
using System.Web.Mvc;

namespace ContosoUniversity.AcceptanceTests.Infrastructure
{
    public class ViewControllerScenario<TController, TViewModel>
        : ControllerScenario<TController, ViewResult, TViewModel>
        where TController : Controller
        where TViewModel : class
    {
        public override void ExecuteAction(Func<TController, ViewResult> func)
        {
            base.ExecuteAction(func);
            try
            {
                Model = (TViewModel)ActionResult.Model;
            }
            catch (InvalidCastException) { }
        }
    }
}