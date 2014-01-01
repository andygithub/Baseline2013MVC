Imports System.Net


Public Class ErrorController
    Inherits Controller
    Public Function NotFound() As ActionResult
        Response.StatusCode = CInt(HttpStatusCode.NotFound)
        Return View()
    End Function

    Public Function ServerError() As ActionResult
        Response.StatusCode = CInt(HttpStatusCode.InternalServerError)

        ' Todo: Pass the exception into the view model, which you can make.
        ' log excetpion
        If True Then
            Dim exception = Server.GetLastError()
            ' etc..
        End If

        Return View()
    End Function

    'secret test method 
    Public Function ThrowError() As ActionResult
        Throw New NotImplementedException("ThrowError")
    End Function
End Class

