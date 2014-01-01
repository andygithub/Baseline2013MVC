Imports System.Configuration
Imports System.Data.Common
Imports System.Data.Objects
Imports System.Reflection
Imports System.Data.Entity.ModelConfiguration
Imports System.Data.Entity
Imports System.Data.Entity.Infrastructure
Imports EFProviderWrapperToolkit
Imports EFTracingProvider
Imports EFCachingProvider.Caching

Namespace Infrastructure

    ''ToDo cleanup this class and add rest of settings to context setup.
    Public Class DbContextBuilder(Of T As DbContext)
        'Inherits DbModelBuilder
        Implements IDbContextBuilder(Of T)

        'Private ReadOnly _factory As DbProviderFactory
        Private ReadOnly _cnStringSettings As ConnectionStringSettings
        'Private ReadOnly _recreateDatabaseIfExists As Boolean
        Private ReadOnly _lazyLoadingEnabled As Boolean


        Public Sub New(lazyLoadingEnabled As Boolean, connectionStringName As String)
            _cnStringSettings = ConfigurationManager.ConnectionStrings(connectionStringName)
            _lazyLoadingEnabled = lazyLoadingEnabled
        End Sub

        ''' <summary>
        ''' Creates a new DbContext.  This maps back to the default generated context type.  This could be overridden with a type parameter or specifying a context on the repository. "/>.
        ''' </summary>
        ''' <returns></returns>
        Public Function BuildDbContext(Optional modelType As Type = Nothing) As T Implements IDbContextBuilder(Of T).BuildDbContext
            'Dim ctx As ObjectContext = Model.SimpleEntities
            'Return New DbContext()
            'Return DirectCast(New Model.SimpleEntities, T)
            'Dim cn = _factory.CreateConnection()
            'cn.ConnectionString = _cnStringSettings.ConnectionString

            'Dim dbModel = Me.Build(cn)

            'Dim ctx As ObjectContext = DbModel.Compile().CreateObjectContext(Of ObjectContext)(cn)
            Dim ctx As ObjectContext

            'Dim ctx As New ObjectContext(EntityConnectionWrapperUtils.CreateEntityConnectionWithWrappers("name=" & _cnStringSettings.Name, "EFTracingProvider", "EFCachingProvider"))



            If modelType Is Nothing Then
                If _cnStringSettings Is Nothing Then
                    ctx = DirectCast(New Model.ExtendedSimpleEntities, IObjectContextAdapter).ObjectContext
                Else

                    ctx = DirectCast(New Model.ExtendedSimpleEntities(_cnStringSettings.Name), IObjectContextAdapter).ObjectContext
                End If
            Else
                ctx = DirectCast(Activator.CreateInstance(modelType), IObjectContextAdapter).ObjectContext
            End If
            'Dim ctx As ObjectContext = DirectCast(New Model.ExtendedSimpleEntities, IObjectContextAdapter).ObjectContext

            ctx.ContextOptions.LazyLoadingEnabled = Me._lazyLoadingEnabled
            'initizing the provider configuration at this point is too late

            Return DirectCast(New DbContext(ctx, True), T)
        End Function


    End Class
End Namespace