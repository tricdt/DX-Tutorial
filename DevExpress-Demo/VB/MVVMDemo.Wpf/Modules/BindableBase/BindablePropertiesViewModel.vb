Imports DevExpress.Mvvm

Namespace MVVMDemo.BindableBaseDemo

    Public Class BindablePropertiesViewModel
        Inherits BindableBase

        Public Property FirstName As String
            Get
                Return GetValue(Of String)()
            End Get

            Set(ByVal value As String)
                SetValue(value, changedCallback:=New System.Action(AddressOf NotifyFullNameChanged))
            End Set
        End Property

        Public Property LastName As String
            Get
                Return GetValue(Of String)()
            End Get

            Set(ByVal value As String)
                SetValue(value, changedCallback:=New System.Action(AddressOf NotifyFullNameChanged))
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
