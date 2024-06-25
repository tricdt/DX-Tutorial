Imports DevExpress.Data.Filtering
Imports DevExpress.Xpf.DemoBase

Namespace ChartsDemo

    <CodeFile("Modules/Highlighted/Waterfall.xaml"), CodeFile("Modules/Highlighted/Waterfall.xaml.(cs)"), NoAutogeneratedCodeFiles>
    Public Partial Class WaterfallDemo
        Inherits ChartsDemoModule

        Public Sub New()
            InitializeComponent()
            DataContext = ReadCarbonData("carbon.csv")
            aggregatedDataItem.Tag = New BinaryOperator("Factor", "Imbalance", BinaryOperatorType.Equal)
            detailedDataItem.Tag = New BinaryOperator("Factor", "Imbalance", BinaryOperatorType.NotEqual)
        End Sub
    End Class
End Namespace