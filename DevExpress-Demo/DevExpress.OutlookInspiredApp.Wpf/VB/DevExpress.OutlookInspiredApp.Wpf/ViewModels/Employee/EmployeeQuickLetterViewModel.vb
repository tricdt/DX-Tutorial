Imports DevExpress.DevAV.DevAVDbDataModel
Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.POCO

Namespace DevExpress.DevAV.ViewModels

    Public Class EmployeeQuickLetterViewModel

        Public Shared Function Create() As EmployeeQuickLetterViewModel
            Return ViewModelSource.Create(Function() New EmployeeQuickLetterViewModel())
        End Function

        Protected Sub New()
        End Sub

        Public Overridable Property Entity As Employee

        Protected ReadOnly Property DocumentManagerService As IDocumentManagerService
            Get
                Return GetRequiredService(Of IDocumentManagerService)()
            End Get
        End Property

        Public Sub ShowMailMerge()
            QuickLetter(String.Empty)
        End Sub

        Public Sub QuickLetter(ByVal templateName As String)
            Dim mailMergeViewModel = ViewModels.MailMergeViewModel(Of Employee, LinksViewModel).Create(GetUnitOfWorkFactory(), Function(x) x.Employees, If(Entity Is Nothing, CType(Nothing, Long?), Entity.Id), templateName, LinksViewModel.Create())
            DocumentManagerService.CreateDocument("EmployeeMailMergeView", mailMergeViewModel, Nothing, Me).Show()
        End Sub

        Public Function CanQuickLetter(ByVal templateName As String) As Boolean
            Return Entity IsNot Nothing
        End Function

        Protected Overridable Sub OnEntityChanged()
            RaiseCanExecuteChanged(Sub(x) x.QuickLetter(""))
        End Sub
    End Class
End Namespace
