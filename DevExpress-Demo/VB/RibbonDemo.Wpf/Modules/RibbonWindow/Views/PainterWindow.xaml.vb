Imports System.Windows
Imports System.Windows.Controls
Imports DevExpress.Xpf.Bars
Imports System.ComponentModel

Namespace RibbonDemo

    Public Partial Class PainterWindow

        Public Sub New()
            InitializeComponent()
        End Sub

        Private Sub OnEditValueChanged(ByVal sender As Object, ByVal e As RoutedEventArgs)
            NavigationTree.ExitMenuMode(True, False)
        End Sub
    End Class
End Namespace
