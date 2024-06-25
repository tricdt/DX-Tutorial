Imports System
Imports System.Globalization
Imports System.Windows.Media
Imports System.Windows.Threading
Imports DevExpress.Xpf.DemoBase
Imports DevExpress.Xpf.Editors

Namespace ChartsDemo

    <CodeFile("Modules/DataBinding/EmptyPoints.xaml"), CodeFile("Modules/DataBinding/EmptyPoints.xaml.(cs)"), CodeFile("Modules/SeriesInfo.(cs)"), CodeFile("DataModels/EmptyPointsDemoData.(cs)"), NoAutogeneratedCodeFiles>
    Public Partial Class EmptyPointsDemo
        Inherits ChartsDemoModule

        Public Sub New()
            InitializeComponent()
        End Sub

        Private Sub lbSeriesGroup_EditValueChanged(ByVal sender As Object, ByVal e As EditValueChangedEventArgs)
            Dispatcher.BeginInvoke(New Action(AddressOf chart.Animate), DispatcherPriority.Background)
        End Sub
    End Class

    Friend Class EmptyPointBrushConverter
        Inherits ForwardOnlyValueConverter

        Public Overrides Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object
            If value IsNot Nothing AndAlso TypeOf value Is Color Then
                Dim color As Color = CType(value, Color)
                Dim brush As SolidColorBrush = New SolidColorBrush(Color.FromArgb(100, color.R, color.G, color.B))
                brush.Freeze()
                Return brush
            End If

            Return value
        End Function
    End Class
End Namespace
