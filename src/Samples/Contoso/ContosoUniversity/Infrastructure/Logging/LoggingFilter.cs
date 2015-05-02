using System.Web.Mvc;
using ContosoUniversity.Logging;

namespace ContosoUniversity.Infrastructure.Logging
{
    public class LoggingFilter : ActionFilterAttribute, IActionFilter
    {
        private static readonly ILog Logger = LogProvider.For<LoggingFilter>();

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            var actionName = filterContext.ActionDescriptor.ActionName;
            var ip = filterContext.HttpContext.Request.UserHostAddress;
            var dateTime = filterContext.HttpContext.Timestamp;

            var message = string.Format("Time: {0}, IP: {1}, Controller: {2}, Action: {3}", dateTime, ip, controllerName,
                actionName);
            Logger.Info(message);

            base.OnActionExecuting(filterContext);
        }
    }
}