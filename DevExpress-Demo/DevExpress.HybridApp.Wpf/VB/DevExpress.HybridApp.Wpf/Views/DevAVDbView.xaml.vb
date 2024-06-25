Imports System
Imports System.Windows
Imports System.Windows.Controls

Namespace DevExpress.DevAV.Views

    Public Partial Class DevAVDbView
        Inherits UserControl

        Public Sub New()
            Me.InitializeComponent()
        End Sub

        Private Sub OnNavButtonCloseClick(ByVal sender As Object, ByVal e As EventArgs)
            Call Application.Current.MainWindow.Close()
        End Sub
    End Class
End Namespace
