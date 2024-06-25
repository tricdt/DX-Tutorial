Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.POCO

Namespace DevExpress.DevAV.ViewModels

    Partial Class EmployeeViewModel

        Private contactsField As EmployeeContactsViewModel

        Private firstName As String

        Private lastName As String

        Public Overrides Sub OnLoaded()
            MyBase.OnLoaded()
            firstName = Entity.FirstName
            lastName = Entity.LastName
        End Sub

        Protected Overrides Function SaveCore() As Boolean
            If Not Equals(Entity.FirstName, firstName) OrElse Not Equals(Entity.LastName, lastName) Then Entity.FullName = Entity.FirstName & " " & Entity.LastName
            Return MyBase.SaveCore()
        End Function

        Public Sub ShowMailMerge()
            Dim mailMergeViewModel = ViewModels.MailMergeViewModel(Of Employee, Object).Create(UnitOfWorkFactory, getRepositoryFunc, Entity.Id)
            DocumentManagerService.CreateDocument("EmployeeMailMergeView", mailMergeViewModel, Nothing, Me).Show()
        End Sub

        Public Sub ShowProfile()
            Log("HybridApp: Create Report : Employee: Profile")
            DocumentManagerService.CreateDocument("ReportPreview", ReportPreviewViewModel.Create(EmployeeProfile(Entity)), Nothing, Me).Show()
        End Sub

        Public Sub ShowMeeting()
            MessageBoxService.ShowMessage(String.Format("Schedule meeting with {0}?", Entity.FullName), "Meeting", MessageButton.YesNoCancel)
        End Sub

        Public ReadOnly Property Contacts As EmployeeContactsViewModel
            Get
                If contactsField Is Nothing Then contactsField = EmployeeContactsViewModel.Create().SetParentViewModel(Me)
                Return contactsField
            End Get
        End Property

        Protected Overrides Sub OnEntityChanged()
            MyBase.OnEntityChanged()
            Contacts.Entity = Entity
            If Entity IsNot Nothing Then Log(String.Format("HybridApp: Edit Employee: {0}", If(String.IsNullOrEmpty(Entity.FullName), "<New>", Entity.FullName)))
        End Sub

        Protected Overrides Function GetTitle() As String
            Return Entity.FullName
        End Function

        Private ReadOnly Property DocumentManagerService As IDocumentManagerService
            Get
                Return GetRequiredService(Of IDocumentManagerService)("FrameDocumentUIService")
            End Get
        End Property
    End Class
End Namespace
