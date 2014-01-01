
Public Class ContactRepository
    Inherits GenericRepository
    Implements IContactRepository
    Public Sub New()
        MyBase.New()
    End Sub

    ''' <summary>
    ''' Initializes a new instance of the <see cref="GenericRepository"/> class.
    ''' </summary>
    ''' <param name="connectionStringName">Name of the connection string.</param>
    Public Sub New(connectionStringName As String)
        MyBase.New(connectionStringName)
    End Sub

    Public Sub New(context As System.Data.Entity.DbContext)
        MyBase.New(context)
    End Sub

    Public Function NewlyCreated() As IList(Of Domain.Contact) Implements IContactRepository.NewlyCreated
        Dim lastMonth = DateTime.Now.[Date].AddMonths(-1)

        Return GetQuery(Of Domain.Contact)().Where(Function(c) c.AddDate >= lastMonth).ToList()
    End Function

    Public Function FindByName(firstname As String, lastname As String) As Domain.Contact Implements IContactRepository.FindByName
        Return GetQuery(Of Domain.Contact)().Where(Function(c) c.FirstName = firstname AndAlso c.LastName = lastname).FirstOrDefault()
    End Function
End Class
