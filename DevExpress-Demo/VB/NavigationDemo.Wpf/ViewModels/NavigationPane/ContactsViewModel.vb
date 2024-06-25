Imports DevExpress.Mvvm
Imports System.Collections.Generic

Namespace NavigationDemo

    Public Class ContactsViewModel
        Inherits DevExpress.Mvvm.ViewModelBase

        Public Overridable Property FirstName As String

        Public Overridable Property LastName As String

        Public Overridable Property Email As String

        Public Overridable Sub AddContact()
            Dim viewModel = CType(Me.Parameter, NavigationDemo.ContactsNavigationViewModel)
            Dim contacts = New System.Collections.Generic.List(Of NavigationDemo.ContactItem)(viewModel.Contacts)
            contacts.Add(New NavigationDemo.ContactItem() With {.FirstName = Me.FirstName, .LastName = Me.LastName, .Email = Me.Email})
            viewModel.Contacts = contacts
            Me.FirstName = String.Empty
            Me.LastName = String.Empty
            Me.Email = String.Empty
        End Sub

        Public Overridable Function CanAddContact() As Boolean
            Return Not(String.IsNullOrEmpty(Me.FirstName) OrElse String.IsNullOrEmpty(Me.FirstName))
        End Function
    End Class
End Namespace
