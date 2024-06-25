Imports System.Threading.Tasks
Imports DevExpress.Xpf.DemoBase.Helpers

Namespace PivotGridDemo

    Public Partial Class MainWindow
        Inherits SidebarWindow

        Public Sub New()
#If DXCORE3
            System.Data.Common.DbProviderFactories.RegisterFactory("Microsoft.Data.Sqlite", Microsoft.Data.Sqlite.SqliteFactory.Instance);
#End If
            InitializeComponent()
        End Sub

        Shared Sub New()
            Call New TaskFactory().StartNew(Function() DevExpress.DemoData.Models.NWindContext.Preload())
        End Sub
    End Class
End Namespace
