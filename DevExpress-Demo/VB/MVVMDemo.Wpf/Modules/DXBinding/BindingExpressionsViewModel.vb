Imports System.Windows

Namespace MVVMDemo.DXBindingDemo

    Public Class BindingExpressionsViewModel

        Public Overridable Property FirstName As String

        Public Overridable Property LastName As String

        Public Overridable Property IsReadonly As Boolean

        Public Overridable Property UserName As String

        Public Overridable Property AcceptTerms As Boolean

        Public Sub Register()
            MessageBox.Show("Registered")
        End Sub

        Protected Sub New()
            FirstName = "Gregory"
            LastName = "Price"
        End Sub
    End Class
End Namespace
