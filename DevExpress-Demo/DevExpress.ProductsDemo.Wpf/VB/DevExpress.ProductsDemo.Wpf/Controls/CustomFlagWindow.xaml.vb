Imports DevExpress.Xpf.Core
Imports System.Windows
Imports System.Windows.Controls

Namespace ProductsDemo.Controls

    Public Partial Class CustomFlagWindow
        Inherits ThemedWindow

        Public Sub New()
            Me.InitializeComponent()
        End Sub

        Private Sub btn_Ok_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            DialogResult = True
            Close()
        End Sub

        Private Sub btn_Cancel_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            DialogResult = False
            Close()
        End Sub
    End Class
End Namespace
