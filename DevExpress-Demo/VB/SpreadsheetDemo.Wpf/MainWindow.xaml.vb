Imports DevExpress.Xpf.Core
Imports DevExpress.Xpf.DemoBase.Helpers

Namespace SpreadsheetDemo

    Public Partial Class MainWindow
        Inherits SidebarWindow

        Public Sub New()
            InitializeComponent()
            SetValue(ScrollBarExtensions.AllowMiddleMouseScrollingProperty, True)
        End Sub
    End Class
End Namespace
