Namespace GridDemo

    Public Partial Class ExcelStyleFiltering
        Inherits GridDemoModule

        Public Sub New()
            InitializeComponent()
            grid.ItemsSource = GenerateVehicleOrders(10000)
        End Sub
    End Class
End Namespace
