'Imports StackExchange.Profiling

'Namespace Filters

'    Public Class ProfilingActionFilter
'        Inherits ActionFilterAttribute

'        Const stackKey As String = "ProfilingActionFilterStack"

'        Public Overrides Sub OnActionExecuting(filterContext As ActionExecutingContext)
'            Dim mp = MiniProfiler.Current
'            If mp IsNot Nothing Then
'                Dim stack = TryCast(HttpContext.Current.Items(stackKey), Stack(Of IDisposable))
'                If stack Is Nothing Then
'                    stack = New Stack(Of IDisposable)()
'                    HttpContext.Current.Items(stackKey) = stack
'                End If

'                Dim prof = MiniProfiler.Current.[Step]("Controller: " & filterContext.Controller.ToString() & "." & Convert.ToString(filterContext.ActionDescriptor.ActionName))

'                stack.Push(prof)
'            End If
'            MyBase.OnActionExecuting(filterContext)
'        End Sub

'        Public Overrides Sub OnActionExecuted(filterContext As ActionExecutedContext)
'            MyBase.OnActionExecuted(filterContext)
'            Dim stack = TryCast(HttpContext.Current.Items(stackKey), Stack(Of IDisposable))
'            If stack IsNot Nothing AndAlso stack.Count > 0 Then
'                stack.Pop().Dispose()
'            End If
'        End Sub

'    End Class

'End Namespace