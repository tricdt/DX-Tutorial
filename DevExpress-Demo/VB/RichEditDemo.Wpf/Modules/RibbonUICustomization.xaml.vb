Imports DevExpress.Xpf.Core
Imports System.Windows

Namespace RichEditDemo

    Public Partial Class RibbonUICustomization
        Inherits RichEditDemoModule

        Public Sub New()
            InitializeComponent()
        End Sub

        Private Sub About_ItemClick(ByVal sender As Object, ByVal e As DevExpress.Xpf.Bars.ItemClickEventArgs)
            ThemedMessageBox.Show("Rich Text Editor Ribbon Customization", "This demo illustrates how to customize the WPF Rich Text Editor's integrated ribbon UI." & Microsoft.VisualBasic.Constants.vbLf & Microsoft.VisualBasic.Constants.vbLf & "Switch to the Code View to learn how to use the Rich Text Editor's RibbonActions collection to create, remove or modify ribbon elements.", MessageBoxButton.OK, MessageBoxImage.Information)
        End Sub
    End Class
End Namespace
