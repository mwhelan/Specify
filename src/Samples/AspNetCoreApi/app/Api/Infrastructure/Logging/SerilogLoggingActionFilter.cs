using Microsoft.AspNetCore.Mvc.Filters;
using Serilog;

namespace ApiTemplate.Api.Infrastructure.Logging
{
    // MVC-specific features like the action method ID, RazorPages Handler name,
    // or the ModelValidationState are only available in an MVC context,
    // so can't be directly accessed by Serilog's middleware.
    public class SerilogLoggingActionFilter : IActionFilter
    {
        private readonly IDiagnosticContext _diagnosticContext;
        public SerilogLoggingActionFilter(IDiagnosticContext diagnosticContext)
        {
            _diagnosticContext = diagnosticContext;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            _diagnosticContext.Set("RouteData", context.ActionDescriptor.RouteValues);
            _diagnosticContext.Set("ActionName", context.ActionDescriptor.DisplayName);
            _diagnosticContext.Set("ActionId", context.ActionDescriptor.Id);
            _diagnosticContext.Set("ValidationState", context.ModelState.IsValid);
        }

        // Required by the interface
        public void OnActionExecuted(ActionExecutedContext context) { }
    }
}
