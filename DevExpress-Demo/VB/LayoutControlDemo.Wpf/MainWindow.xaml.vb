Imports System.Threading.Tasks
Imports DevExpress.Xpf.DemoBase.Helpers

Namespace LayoutControlDemo

    Public Partial Class MainWindow
        Inherits SidebarWindow

        Public Sub New()
            InitializeComponent()
        End Sub

        Shared Sub New()
            Call New TaskFactory().StartNew(Function() DevExpress.DemoData.Models.Vehicles.VehiclesContext.Preload())
        End Sub
    End Class
End Namespace
