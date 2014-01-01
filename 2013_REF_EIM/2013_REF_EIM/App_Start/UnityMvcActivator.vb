Imports System.Linq
Imports System.Web.Mvc
'Imports Microsoft.Practices.Unity.Mvc

<Assembly: WebActivatorEx.PreApplicationStartMethod(GetType(_2013_REF_EIM.App_Start.UnityWebActivator), "Start")> 

Namespace _2013_REF_EIM.App_Start
    ''' <summary>Provides the bootstrapping for integrating Unity with ASP.NET MVC.</summary>
    Public NotInheritable Class UnityWebActivator
        Private Sub New()
        End Sub
        ''' <summary>Integrates Unity when the application starts.</summary>
        Public Shared Sub Start()
            Dim _container = Container.ContainerFactory.GetConfiguredContainer()

            FilterProviders.Providers.Remove(FilterProviders.Providers.OfType(Of FilterAttributeFilterProvider)().First())
            FilterProviders.Providers.Add(New Microsoft.Practices.Unity.Mvc.UnityFilterAttributeFilterProvider(_container))

            DependencyResolver.SetResolver(New Unity.Mvc4.UnityDependencyResolver(_container))

            ' TODO: Uncomment if you want to use PerRequestLifetimeManager
            ' Microsoft.Web.Infrastructure.DynamicModuleHelper.DynamicModuleUtility.RegisterModule(typeof(UnityPerRequestHttpModule));
        End Sub
    End Class
End Namespace