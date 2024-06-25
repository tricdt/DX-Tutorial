Imports System.Threading.Tasks
Imports DevExpress.Xpf.Core
Imports DevExpress.Xpf.DemoBase.Helpers

Namespace TreeListDemo

    Public Partial Class MainWindow
        Inherits SidebarWindow

        Public Sub New()
            InitializeComponent()
            SetValue(ScrollBarExtensions.AllowMiddleMouseScrollingProperty, True)
        End Sub

        Shared Sub New()
            Call New TaskFactory().StartNew(Function() DevExpress.DemoData.Models.NWindContext.Preload())
        End Sub
    End Class
End Namespace
