Imports DevExpress.Mvvm

Namespace MVVMDemo.DXEventDemo

    Public Class BindingEventToMethodViewModel
        Inherits BindableBase

        Public Property State As String
            Get
                Return GetValue(Of String)()
            End Get

            Private Set(ByVal value As String)
                SetValue(value)
            End Set
        End Property

        Public Sub Initialize()
            State = "Initialized"
        End Sub
    End Class
End Namespace
