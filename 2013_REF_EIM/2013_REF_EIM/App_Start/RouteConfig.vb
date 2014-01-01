Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Web.Mvc
Imports System.Web.Routing

Public Module RouteConfig
    Public Sub RegisterRoutes(ByVal routes As RouteCollection)
        routes.IgnoreRoute("{resource}.axd/{*pathInfo}")
        routes.IgnoreRoute("{*favicon}", New With {.favicon = "(.*/)?favicon.ico(/.*)?"})

        '    /NotFound <- for a 404 not found, error page.
        '/ServerError <- for any other error, include errors that happen in code. this is a 500 Internal Server Error
        'This order is important

        routes.MapRoute(
        name:="Error - 404",
        url:="NotFound",
        defaults:=New With {.controller = "Error", .action = "NotFound"}
        )

        routes.MapRoute(
           name:="Error - 500",
            url:="ServerError",
            defaults:=New With {.controller = "Error", .action = "ServerError"}
            )

        routes.MapRoute(
            name:="Default",
            url:="{controller}/{action}/{id}",
            defaults:=New With {.controller = "Home", .action = "Index", .id = UrlParameter.Optional}
        )


    End Sub
End Module