Imports DevExpress.Mvvm
Imports DevExpress.Xpf.Charts

Namespace ChartsDemo

    Public Partial Class RegularGridDataTab
        Inherits TabItemModule

        Public Sub New()
            InitializeComponent()
        End Sub
    End Class

    Public Class FillStyleItem
        Inherits BindableBase

        Private titleField As String

        Private fillStyleField As FillStyleBase

        Public Property Title As String
            Get
                Return titleField
            End Get

            Set(ByVal value As String)
                SetProperty(titleField, value, Function() Title)
            End Set
        End Property

        Public Property FillStyle As FillStyleBase
            Get
                Return fillStyleField
            End Get

            Set(ByVal value As FillStyleBase)
                SetProperty(fillStyleField, value, Function() FillStyle)
            End Set
        End Property
    End Class
End Namespace
