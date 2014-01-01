
Public Interface IContactRepository
    Inherits IRepository
    Function NewlyCreated() As IList(Of Domain.Contact)

    Function FindByName(firstname As String, lastname As String) As Domain.Contact

End Interface
