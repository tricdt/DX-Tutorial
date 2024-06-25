Imports DevExpress.DemoData.Models
#If DXCORE3
using Microsoft.EntityFrameworkCore;
#Else
Imports System.Data.Entity

#End If
Namespace GridDemo

    Public Partial Class MasterDetailViewViaEntityFramework
        Inherits GridDemoModule

        Public Sub New()
            InitializeComponent()
            Dim customers = New NWindContext().Customers
            customers.Load()
            grid.ItemsSource = customers.Local
        End Sub
    End Class
End Namespace
