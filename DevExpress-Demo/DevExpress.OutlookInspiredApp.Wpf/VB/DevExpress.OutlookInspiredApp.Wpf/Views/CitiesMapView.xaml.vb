Imports DevExpress.Xpf.Map
Imports System.Windows.Controls

Namespace DevExpress.DevAV.Views

    Public Partial Class CitiesMapView
        Inherits UserControl

        Public Sub New()
            Me.InitializeComponent()
        End Sub

        Private Sub OnWebRequest(ByVal sender As Object, ByVal e As MapWebRequestEventArgs)
            e.UserAgent = "DevExpress WPF Map Control Main Demo"
        End Sub
    End Class
End Namespace
