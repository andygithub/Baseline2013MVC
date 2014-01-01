Imports System.Data
Imports System.Data.Objects

Namespace Infrastructure
    Public Interface IUnitOfWork
        Inherits IDisposable
        ReadOnly Property IsInTransaction() As Boolean

        Sub SaveChanges()

        Sub SaveChanges(saveOptions As SaveOptions)

        Sub BeginTransaction()

        Sub BeginTransaction(isolationLevel As IsolationLevel)

        Sub RollBackTransaction()

        Sub CommitTransaction()
    End Interface
End Namespace