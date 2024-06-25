Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.DataAnnotations
Imports System.ComponentModel.DataAnnotations
Imports System.Windows

Namespace MVVMDemo.POCOViewModels

    <POCOViewModel(ImplementIDataErrorInfo:=True)>
    Public Class DataErrorInfoViewModel

        <Required(ErrorMessage:="Please enter the user name.")>
        <StringLength(100, MinimumLength:=5)>
        Public Overridable Property UserName As String

        Public Sub Register()
            MessageBox.Show("Registered")
        End Sub

        Public Function CanRegister() As Boolean
            Return Not IDataErrorInfoHelper.HasErrors(Me)
        End Function
    End Class
End Namespace
