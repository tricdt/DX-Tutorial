Imports System.Threading.Tasks
Imports DevExpress.DemoData.Models
Imports DevExpress.Xpf.DemoBase.Helpers

Namespace PropertyGridDemo

    Public Partial Class MainWindow
        Inherits SidebarWindow

        Public Sub New()
            InitializeComponent()
        End Sub

        Shared Sub New()
            Call New TaskFactory().StartNew(Function() NWindContext.Preload())
        End Sub
    End Class
End Namespace
