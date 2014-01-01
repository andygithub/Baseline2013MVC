
Public Class HomeController
    Inherits System.Web.Mvc.Controller

    Sub New(logger As Reference.EIM.Core.Logging.ILogger)
        If logger Is Nothing Then Throw New ArgumentNullException("logger")
        _logger = logger
    End Sub

    Private _logger As Reference.EIM.Core.Logging.ILogger

    Function Index() As ActionResult
        _logger.Debug("Controller Index - {0}", _logger.GetType.ToString)
        _logger.Trace("Index Controller internal step: ")

        'Dim borker As Glimpse.Core.Extensibility.IMessageBroker = Glimpse.Core.Framework.GlimpseConfiguration.GetConfiguredMessageBroker

        Return View()
    End Function

    Function About() As ActionResult
        ViewData("Message") = "Your application description page."

        Return View()
    End Function

    Function Contact() As ActionResult
        ViewData("Message") = "Your contact page."

        Return View()
    End Function
End Class
