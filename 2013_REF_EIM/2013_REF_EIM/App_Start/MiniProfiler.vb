'Imports System.Web
'Imports System.Web.Mvc
'Imports System.Linq
'Imports StackExchange.Profiling
''Imports StackExchange.Profiling.M
'Imports Microsoft.Web.Infrastructure
'Imports Microsoft.Web.Infrastructure.DynamicModuleHelper
''using System.Data;
''using System.Data.Entity;
''using System.Data.Entity.Infrastructure;
''using StackExchange.Profiling.Data.EntityFramework;
''using StackExchange.Profiling.Data.Linq2Sql;

'<Assembly: WebActivator.PreApplicationStartMethod(GetType(_2013_REF_EIM.App_Start.MiniProfilerPackage), "PreStart")> 
'<Assembly: WebActivator.PostApplicationStartMethod(GetType(_2013_REF_EIM.App_Start.MiniProfilerPackage), "PostStart")> 

'Namespace _2013_REF_EIM.App_Start
'    Public NotInheritable Class MiniProfilerPackage
'        Private Sub New()
'        End Sub
'        Public Shared Sub PreStart()

'            ' Be sure to restart you ASP.NET Developement server, this code will not run until you do that. 

'            'TODO: See - _MINIPROFILER UPDATED Layout.cshtml
'            '      For profiling to display in the UI you will have to include the line @StackExchange.Profiling.MiniProfiler.RenderIncludes() 
'            '      in your master layout

'            'TODO: Non SQL Server based installs can use other formatters like: new StackExchange.Profiling.SqlFormatters.InlineFormatter()
'            MiniProfiler.Settings.SqlFormatter = New StackExchange.Profiling.SqlFormatters.SqlServerFormatter()

'            'TODO: To profile a standard DbConnection: 
'            ' var profiled = new ProfiledDbConnection(cnn, MiniProfiler.Current);

'            'TODO: If you are profiling EF code first try: 
'            ' MiniProfilerEF.Initialize();

'            'Make sure the MiniProfiler handles BeginRequest and EndRequest
'            DynamicModuleUtility.RegisterModule(GetType(MiniProfilerStartupModule))

'            'Setup profiler for Controllers via a Global ActionFilter
'            GlobalFilters.Filters.Add(New Filters.ProfilingActionFilter())

'            ' You can use this to check if a request is allowed to view results
'            'MiniProfiler.Settings.Results_Authorize = (request) =>
'            '{
'            ' you should implement this if you need to restrict visibility of profiling on a per request basis 
'            '    return !DisableProfilingResults; 
'            '};

'            ' the list of all sessions in the store is restricted by default, you must return true to alllow it
'            'MiniProfiler.Settings.Results_List_Authorize = (request) =>
'            '{
'            ' you may implement this if you need to restrict visibility of profiling lists on a per request basis 
'            'return true; // all requests are kosher
'            '};
'        End Sub

'        Public Shared Sub PostStart()
'            ' Intercept ViewEngines to profile all partial views and regular views.
'            ' If you prefer to insert your profiling blocks manually you can comment this out
'            Dim copy = ViewEngines.Engines.ToList()
'            ViewEngines.Engines.Clear()
'            For Each item In copy
'                ViewEngines.Engines.Add(New ProfilingViewEngine(item))
'            Next
'        End Sub
'    End Class

'    Public Class MiniProfilerStartupModule
'        Implements IHttpModule
'        Public Sub Init(context As HttpApplication) Implements IHttpModule.Init
'            'AddHandler context.BeginRequest, Sub(sender, e)
'            '                                     Dim request = DirectCast(sender, HttpApplication).Request
'            '                                     'TODO: By default only local requests are profiled, optionally you can set it up
'            '                                     '  so authenticated users are always profiled
'            '                                     If request.IsLocal Then
'            '                                         MiniProfiler.Start()
'            '                                     End If
'            '                                 End Sub


'            ' TODO: You can control who sees the profiling information
'            '
'            '            context.AuthenticateRequest += (sender, e) =>
'            '            {
'            '                if (!CurrentUserIsAllowedToSeeProfiler())
'            '                {
'            '                    StackExchange.Profiling.MiniProfiler.Stop(discardResults: true);
'            '                }
'            '            };
'            '            


'            'AddHandler context.EndRequest, Sub(sender, e)
'            '                                   MiniProfiler.[Stop]()
'            '                               End Sub
'        End Sub

'        Public Sub Dispose() Implements IHttpModule.Dispose
'        End Sub
'    End Class
'End Namespace
