Imports System.Windows
Imports DevExpress.Xpf.DemoBase
Imports DevExpress.Xpf.WindowsUI

Namespace CommonDemo

    Public Partial Class Windows8StyleMessageBox
        Inherits DemoModule

        Public Sub New()
            InitializeComponent()
        End Sub

        Private Sub Button_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Call WinUIMessageBox.Show(contentEdit.DisplayText, captionEdit.DisplayText, CType(buttons.EditValue, MessageBoxButton), CType(icons.EditValue, MessageBoxImage), MessageBoxResult.None, MessageBoxOptions.None)
        End Sub
    End Class
End Namespace
