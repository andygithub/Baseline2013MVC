Imports System.IO
Imports EFCachingProvider.Caching
Imports EFCachingProvider
Imports EFTracingProvider
Imports EFProviderWrapperToolkit
Imports System.Data.Entity.Infrastructure
Imports System.Data.Objects

Partial Public Class ExtendedSimpleEntities
    Inherits SimpleEntities

    Private logOutput As TextWriter

    Public Sub New()
        Me.New("SimpleEntities")
    End Sub

    Public Sub New(connectionStringName As String)
        MyBase.New(New ObjectContext(EntityConnectionWrapperUtils.CreateEntityConnectionWithWrappers("name=" & connectionStringName, "EFTracingProvider", "EFCachingProvider")))
        'when initialized a wrapper the default container name is not populated which causes issues with the genericRepository and the entity name retreival.
        If String.IsNullOrWhiteSpace(DirectCast(Me, IObjectContextAdapter).ObjectContext.DefaultContainerName) Then
            DirectCast(Me, IObjectContextAdapter).ObjectContext.DefaultContainerName = connectionStringName
        End If
    End Sub

#Region "Tracing Extensions"

    Private ReadOnly Property TracingConnection() As EFTracingConnection
        Get
            'Return  Me.UnwrapConnection(Of EFTracingConnection)()
            Return DirectCast(Me, IObjectContextAdapter).ObjectContext.UnwrapConnection(Of EFTracingConnection)()
        End Get
    End Property

    Public Event CommandExecuting As EventHandler(Of CommandExecutionEventArgs)

    Public Event CommandFinished As EventHandler(Of CommandExecutionEventArgs)

    Public Event CommandFailed As EventHandler(Of CommandExecutionEventArgs)

    Private Sub AppendToLog(sender As Object, e As CommandExecutionEventArgs)
        If Me.logOutput IsNot Nothing Then
            Me.logOutput.WriteLine(e.ToTraceString().TrimEnd())
            Me.logOutput.WriteLine()
        End If
    End Sub

    Public Property Log() As TextWriter
        Get
            Return Me.logOutput
        End Get
        Set(value As TextWriter)
            If (Me.logOutput IsNot Nothing) <> (value IsNot Nothing) Then
                If value Is Nothing Then
                    RemoveHandler CommandExecuting, AddressOf AppendToLog
                Else
                    AddHandler CommandExecuting, AddressOf AppendToLog
                End If
            End If

            Me.logOutput = value
        End Set
    End Property


#End Region

#Region "Caching Extensions"

    Private ReadOnly Property CachingConnection() As EFCachingConnection
        Get
            'Return Me.UnwrapConnection(Of EFCachingConnection)()
            Return DirectCast(Me, IObjectContextAdapter).ObjectContext.UnwrapConnection(Of EFCachingConnection)()
        End Get
    End Property

    Public Property Cache() As ICache
        Get
            Return CachingConnection.Cache
        End Get
        Set(value As ICache)
            CachingConnection.Cache = value
        End Set
    End Property

    Public Property CachingPolicy() As CachingPolicy
        Get
            Return CachingConnection.CachingPolicy
        End Get
        Set(value As CachingPolicy)
            CachingConnection.CachingPolicy = value
        End Set
    End Property

#End Region

End Class
