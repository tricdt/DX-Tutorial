Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.DataAnnotations
Imports System.ComponentModel
Imports System.ComponentModel.DataAnnotations
Imports System.Windows

Namespace MVVMDemo.ViewModelBaseDemo

    Public Class DataErrorInfoViewModel
        Inherits ViewModelBase
        Implements IDataErrorInfo

        <Required(ErrorMessage:="Please enter the user name.")>
        <StringLength(100, MinimumLength:=5)>
        Public Overridable Property UserName As String

        Private ReadOnly Property [Error] As String Implements IDataErrorInfo.[Error]
            Get
                Return Nothing
            End Get
        End Property

        Private ReadOnly Property Item(ByVal columnName As String) As String Implements IDataErrorInfo.Item
            Get
                Return IDataErrorInfoHelper.GetErrorText(Me, columnName)
            End Get
        End Property

        <Command>
        Public Sub Register()
            MessageBox.Show("Registered")
        End Sub

        Public Function CanRegister() As Boolean
            Return Not IDataErrorInfoHelper.HasErrors(Me)
        End Function
    End Class
End Namespace
