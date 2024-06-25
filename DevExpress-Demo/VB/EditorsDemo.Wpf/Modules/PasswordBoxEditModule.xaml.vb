Imports System
Imports System.ComponentModel
Imports System.Windows
Imports System.Windows.Data
Imports DevExpress.Mvvm
Imports DevExpress.Xpf.Editors

Namespace EditorsDemo

    Public Partial Class PasswordBoxEditModule
        Inherits EditorsDemoModule

        Public Sub New()
            InitializeComponent()
            sample.DataContext = New Info()
        End Sub

        Private Sub TextEdit_Validate(ByVal sender As Object, ByVal e As ValidationEventArgs)
            If String.IsNullOrEmpty(TryCast(e.Value, String)) Then
                e.IsValid = False
                e.Handled = True
                e.ErrorContent = "Enter login"
            End If
        End Sub

        Private Sub password_EditValueChanged(ByVal sender As Object, ByVal e As EditValueChangedEventArgs)
            Dim binding As BindingExpression = passwordConfirm.GetBindingExpression(BaseEdit.EditValueProperty)
            If binding Is Nothing Then Return
            binding.UpdateSource()
        End Sub

        Private Sub passwordConfirm_EditValueChanged(ByVal sender As Object, ByVal e As EditValueChangedEventArgs)
        End Sub
    End Class

    Public Class Info
        Inherits BindableBase
        Implements IDataErrorInfo

        Public Property Login As String
            Get
                Return GetProperty(Function() Me.Login)
            End Get

            Set(ByVal value As String)
                SetProperty(Function() Login, value)
            End Set
        End Property

        Public Property Password As String
            Get
                Return GetProperty(Function() Me.Password)
            End Get

            Set(ByVal value As String)
                SetProperty(Function() Password, value)
            End Set
        End Property

        Public Property PasswordConfirm As String
            Get
                Return GetProperty(Function() Me.PasswordConfirm)
            End Get

            Set(ByVal value As String)
                SetProperty(Function() PasswordConfirm, value)
            End Set
        End Property

#Region "IDataErrorInfo Members"
        Public ReadOnly Property [Error] As String Implements IDataErrorInfo.[Error]
            Get
                Return "error"
            End Get
        End Property

        Default Public ReadOnly Property Item(ByVal columnName As String) As String Implements IDataErrorInfo.Item
            Get
                Dim isValidPassword As Boolean = Not String.IsNullOrEmpty(Password) AndAlso Password.Length > 5
                If Equals(columnName, "Password") Then Return If(isValidPassword, String.Empty, "error")
                If Equals(columnName, "PasswordConfirm") Then
                    If Equals(Password, Nothing) Then Return String.Empty
                    Return If(Equals(Password, PasswordConfirm), String.Empty, "Please verify your password")
                End If

                Return String.Empty
            End Get
        End Property
#End Region
    End Class

    Public Class PasswordConfirmVisibilityConverter
        Implements IMultiValueConverter

#Region "IMultiValueConverter Members"
        Private Function Convert(ByVal values As Object(), ByVal targetType As Type, ByVal parameter As Object, ByVal culture As Globalization.CultureInfo) As Object Implements IMultiValueConverter.Convert
            Return If(values(0) IsNot Nothing AndAlso CBool(values(1)), Visibility.Visible, Visibility.Hidden)
        End Function

        Private Function ConvertBack(ByVal value As Object, ByVal targetTypes As Type(), ByVal parameter As Object, ByVal culture As Globalization.CultureInfo) As Object() Implements IMultiValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function
#End Region
    End Class
End Namespace
