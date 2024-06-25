Imports DevExpress.Mvvm

Namespace MVVMDemo.BindableBaseDemo

    Public Class BindablePropertiesViaFieldsViewModel
        Inherits BindableBase

        Private _FirstName As String

        Public Property FirstName As String
            Get
                Return _FirstName
            End Get

            Set(ByVal value As String)
                SetValue(_FirstName, value, changedCallback:=New System.Action(AddressOf NotifyFullNameChanged))
            End Set
        End Property

        Private _LastName As String

        Public Property LastName As String
            Get
                Return _LastName
            End Get

            Set(ByVal value As String)
                SetValue(_LastName, value, changedCallback:=New System.Action(AddressOf NotifyFullNameChanged))
            End Set
        End Property

        Public ReadOnly Property FullName As String
            Get
                Return FirstName & " " & LastName
            End Get
        End Property

        Private Sub NotifyFullNameChanged()
            RaisePropertyChanged(Function() FullName)
        End Sub
    End Class
End Namespace
