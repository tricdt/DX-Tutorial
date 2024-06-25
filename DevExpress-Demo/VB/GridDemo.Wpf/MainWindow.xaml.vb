Imports System.Threading.Tasks
Imports DevExpress.DemoData.Models
Imports DevExpress.DemoData.Models.Vehicles
Imports DevExpress.Xpf.Core
Imports DevExpress.Xpf.DemoBase.Helpers

Namespace GridDemo

    Public Partial Class MainWindow
        Inherits SidebarWindow

        Public Sub New()
            InitializeComponent()
            SetValue(ScrollBarExtensions.AllowMiddleMouseScrollingProperty, True)
#If DXCORE3
            System.Data.Common.DbProviderFactories.RegisterFactory("Microsoft.Data.Sqlite", Microsoft.Data.Sqlite.SqliteFactory.Instance);
#End If
        End Sub

        Shared Sub New()
            Call New TaskFactory().StartNew(Function() NWindContext.Preload()).ContinueWith(Function(t) VehiclesContext.Preload(), TaskContinuationOptions.ExecuteSynchronously)
        End Sub
    End Class
End Namespace
