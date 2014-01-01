

Public Class SessionExpireFilterAttribute
    Inherits ActionFilterAttribute

    'TODO review that tis works a defined and how it works with expired ajax requests.
    Public Overrides Sub OnActionExecuting(filterContext As ActionExecutingContext)
        Dim ctx As HttpContextBase = filterContext.HttpContext

        ' check if session is supported
        If ctx.Session IsNot Nothing Then

            ' check if a new session id was generated
            If ctx.Session.IsNewSession Then

                ' If it says it is a new session, but an existing cookie exists, then it must
                ' have timed out
                Dim sessionCookie As String = ctx.Request.Headers("Cookie")
                If (sessionCookie IsNot Nothing) AndAlso (sessionCookie.IndexOf("ASP.NET_SessionId") >= 0) Then

                    filterContext.Result = OnSessionExpiryRedirectResult(filterContext)
                End If
            End If
        End If

        MyBase.OnActionExecuting(filterContext)
    End Sub

    Public Overridable Function OnSessionExpiryRedirectResult(filterContext As ActionExecutingContext) As ActionResult
        Return New RedirectToRouteResult(New RouteValueDictionary(New With {.action = "Login", .controller = "Account"}))
    End Function

End Class

