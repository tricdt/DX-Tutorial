Imports DevExpress.DemoData.Models.Vehicles
Imports System.Linq

Namespace GridDemo

    Public Partial Class BandedView
        Inherits GridDemoModule

        Public Sub New()
            InitializeComponent()
            grid.ItemsSource = New VehiclesContext().Models.ToList()
        End Sub
    End Class
End Namespace
