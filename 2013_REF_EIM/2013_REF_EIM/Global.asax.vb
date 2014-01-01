Imports System.Web.Optimization

Public Class MvcApplication
    Inherits System.Web.HttpApplication
    Protected Sub Application_Start()
        AreaRegistration.RegisterAllAreas()
        FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters)
        RouteConfig.RegisterRoutes(RouteTable.Routes)
        BundleConfig.RegisterBundles(BundleTable.Bundles)
        'unity initialization is taking place in UnityMvcActivator

        'the profiler is init in the save events below because order of the same events was a problem when using activators

        'EF Provider init should only need to be done once per app domain
        Reference.EIM.Repository.Infrastructure.TracingProviderConfiguration.Init()
        Reference.EIM.Repository.Infrastructure.CachingProviderConfiguration.Init()
    End Sub

    Private _storage As Reference.EIM.Repository.Infrastructure.WebDbContextStorage
    Public Const STORAGE_KEY As String = "HttpContextObjectContextStorageKey"

    'TODO move these events into a custom activator
    'TODO strongly type those actionlinks

    Protected Sub Application_BeginRequest(sender As Object, e As EventArgs)
        Debug.WriteLine("DbContextInitializer.Instance.InitializeDbContextOnce Start ")
        Reference.EIM.Repository.Infrastructure.DbContextInitializer.Instance.InitializeDbContextOnce(Sub()
                                                                                                          Reference.EIM.Repository.Infrastructure.DbContextManager.InitStorage(_storage)
                                                                                                          Reference.EIM.Repository.Infrastructure.DbContextManager.Init(Reference.EIM.Resources.Constants.ConnectionStringKey, False)
                                                                                                      End Sub)
        Debug.WriteLine("DbContextInitializer.Instance.InitializeDbContextOnce End ")
    End Sub

    Private Sub MvcApplication_EndRequest(sender As Object, e As EventArgs) Handles Me.EndRequest
        Debug.WriteLine("DbContextManager.CloseAllDbContexts Start ")
        Reference.EIM.Repository.Infrastructure.DbContextManager.CloseAllDbContexts()
        HttpContext.Current.Items.Remove(STORAGE_KEY)
        Debug.WriteLine("DbContextManager.CloseAllDbContexts End ")

    End Sub

    Public Overrides Sub Init()
        MyBase.Init()
        Debug.WriteLine("WebContextStorage Constructor Start ")
        _storage = New Reference.EIM.Repository.Infrastructure.WebDbContextStorage()
        Debug.WriteLine("WebContextStorage Constructor End ")
    End Sub

    'handled by the MiniProfiler activator
    'the begin and end request event code in the activator was moved into one event method so that order could be guaranteed

    'Private Sub MvcApplication_BeginRequest(sender As Object, e As EventArgs) Handles Me.BeginRequest
    '    'wrap this start in a config check to see if it shoiuld be turned on
    '    'http://miniprofiler.com/
    '    StackExchange.Profiling.MiniProfiler.Start()
    'End Sub

    'Private Sub MvcApplication_EndRequest(sender As Object, e As EventArgs) Handles Me.EndRequest
    '    StackExchange.Profiling.MiniProfiler.Stop()
    'End Sub



End Class

