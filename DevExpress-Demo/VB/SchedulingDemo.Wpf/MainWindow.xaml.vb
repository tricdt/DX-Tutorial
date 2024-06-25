Imports DevExpress.Xpf.DemoBase.Helpers
Imports System.Threading.Tasks

Namespace SchedulingDemo

    Public Partial Class MainWindow
        Inherits SidebarWindow

        Public Sub New()
            InitializeComponent()
        End Sub

        Shared Sub New()
            Call New TaskFactory().StartNew(Function() DevExpress.DemoData.Models.NWindContext.Preload())
        End Sub
    End Class
End Namespace
