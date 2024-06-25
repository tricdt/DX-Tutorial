Imports DevExpress.Mvvm.POCO

Namespace DevExpress.SalesDemo.Wpf.ViewModel

    Public Class ProgressSplashScreenViewModel

        Private _Caption As String, _Copyright As String

        Public Shared Function Create(ByVal caption As String, ByVal copyright As String) As ProgressSplashScreenViewModel
            Return ViewModelSource.Create(Function() New ProgressSplashScreenViewModel(caption, copyright))
        End Function

        Protected Sub New(ByVal caption As String, ByVal copyright As String)
            Me.Caption = caption
            Me.Copyright = copyright
        End Sub

        Public Property Caption As String
            Get
                Return _Caption
            End Get

            Private Set(ByVal value As String)
                _Caption = value
            End Set
        End Property

        Public Property Copyright As String
            Get
                Return _Copyright
            End Get

            Private Set(ByVal value As String)
                _Copyright = value
            End Set
        End Property
    End Class
End Namespace
