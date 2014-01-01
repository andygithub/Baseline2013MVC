
Namespace Filters
    Public Class ConsolerLoggingFilter
        Inherits ActionFilterAttribute
        Implements IActionFilter

        Sub New(logger As Reference.EIM.Core.Logging.ILogger)
            If logger Is Nothing Then Throw New ArgumentNullException("logger")
            _logger = logger
        End Sub

        Dim _logger As Reference.EIM.Core.Logging.ILogger

        'OnActionExecuting(ActionExecutedContext filterContext): Just before the action method is called.
        'OnActionExecuted(ActionExecutingContext filterContext): After the action method is called and before the result is executed (before view render).
        'OnResultExecuting(ResultExecutingContext filterContext): Just before the result is executed (before view render).
        'OnResultExecuted(ResultExecutedContext filterContext): After the result is executed (after the view is rendered).

        Public Overrides Sub OnActionExecuting(filterContext As ActionExecutingContext)
            ' Log Action Filter Call
            _logger.Debug("ConsolerLoggingFilter OnActionExecuting - {0}, {1}, {2}", filterContext.ActionDescriptor.ControllerDescriptor.ControllerName, filterContext.ActionDescriptor.ActionName, filterContext.HttpContext.Timestamp)

            MyBase.OnActionExecuting(filterContext)
        End Sub

        Public Overrides Sub OnActionExecuted(filterContext As System.Web.Mvc.ActionExecutedContext)
            ' Log Action Filter Call
            _logger.Debug("ConsolerLoggingFilter OnActionExecuted - {0}, {1}, {2}", filterContext.ActionDescriptor.ControllerDescriptor.ControllerName, filterContext.ActionDescriptor.ActionName, filterContext.HttpContext.Timestamp)

            MyBase.OnActionExecuted(filterContext)
        End Sub

        Public Overrides Sub OnResultExecuted(filterContext As System.Web.Mvc.ResultExecutedContext)
            ' Log Action Filter Call
            _logger.Debug("ConsolerLoggingFilter OnResultExecuted -  {0}, {1}, {2}", filterContext.RouteData.Values("controller"), filterContext.RouteData.Values("action"), filterContext.HttpContext.Timestamp)

            MyBase.OnResultExecuted(filterContext)
        End Sub

    End Class

End Namespace