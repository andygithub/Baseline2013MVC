
Public Interface IWorkFlowService

    Sub ExecuteWorkFlow(item As Object)
    Function ExecuteWorkFlowItem(item As Object) As Object

End Interface

Public Class WorkFlowService
    Implements IWorkFlowService

    Public Sub ExecuteWorkFlow(item As Object) Implements IWorkFlowService.ExecuteWorkFlow
        Throw New NotImplementedException
    End Sub

    Public Function ExecuteWorkFlowItem(item As Object) As Object Implements IWorkFlowService.ExecuteWorkFlowItem
        Return Nothing
    End Function

End Class

Public Interface IValidationService
    Sub ExecuteValidation(item As Object)
    Function ExecuteAllValidations(item As Object) As Object
End Interface

Public Class ValidationSesrvice
    Implements IValidationService

    Public Function ExecuteAllValidations(item As Object) As Object Implements IValidationService.ExecuteAllValidations
        Return Nothing
    End Function

    Public Sub ExecuteValidation(item As Object) Implements IValidationService.ExecuteValidation
        Throw New NotImplementedException
    End Sub

End Class

