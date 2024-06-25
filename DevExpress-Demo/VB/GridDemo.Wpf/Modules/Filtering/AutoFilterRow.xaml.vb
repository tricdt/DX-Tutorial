Namespace GridDemo.Filtering

    Public Partial Class AutoFilterRow
        Inherits GridDemoModule

        Public Sub New()
            InitializeComponent()
            grid.ItemsSource = CreateOutlookDataList(1000)
        End Sub
    End Class
End Namespace
