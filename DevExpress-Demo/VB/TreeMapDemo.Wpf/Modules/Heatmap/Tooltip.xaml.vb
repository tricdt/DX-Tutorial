Imports DevExpress.Xpf.DemoBase

Namespace TreeMapDemo

    <CodeFile("Modules/Heatmap/Tooltip.xaml")>
    <CodeFile("Modules/Heatmap/Tooltip.xaml.(cs)")>
    <NoAutogeneratedCodeFiles>
    Public Partial Class Tooltip
        Inherits TreeMapDemoModule

        Public Sub New()
            InitializeComponent()
            DataContext = CreateDataSet("/Data/PerformanceMonitoring.xml")
        End Sub
    End Class
End Namespace
