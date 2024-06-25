Imports DevExpress.Mvvm.DataAnnotations
Imports System.ComponentModel.DataAnnotations
Imports System.Windows

Namespace MVVMDemo.Behaviors

    <POCOViewModel(ImplementIDataErrorInfo:=True)>
    Public Class ValidationErrorsHostBehaviorViewModel

        <Required>
        Public Overridable Property FirstName As String

        <Required>
        Public Overridable Property LastName As String

        Public Sub Register()
            MessageBox.Show("Registered")
        End Sub
    End Class
End Namespace
