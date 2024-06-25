Imports System.Collections.Generic
Imports DevExpress.Mvvm.POCO

Namespace NavigationDemo

    Public Class NavigationViewModelBase
    End Class

    Public Class ContactsNavigationViewModel
        Inherits NavigationViewModelBase

        Public Shared Function Create() As ContactsNavigationViewModel
            Return ViewModelSource.Create(Function() New ContactsNavigationViewModel())
        End Function

        Public Overridable Property Contacts As IList(Of ContactItem)

        Protected Sub New()
            Contacts = New List(Of ContactItem)(ContactsData)
        End Sub
    End Class
End Namespace
