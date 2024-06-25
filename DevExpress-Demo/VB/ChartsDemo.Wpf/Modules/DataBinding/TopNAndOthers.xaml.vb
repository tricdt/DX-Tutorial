Imports System
Imports System.Globalization
Imports System.Windows
Imports System.Windows.Data
Imports DevExpress.Xpf.DemoBase

Namespace ChartsDemo

    <CodeFile("Modules/DataBinding/TopNAndOthers.xaml"), CodeFile("Modules/DataBinding/TopNAndOthers.xaml.(cs)"), CodeFile("DataModels/CountriesAreaData.(cs)"), NoAutogeneratedCodeFiles>
    Public Partial Class TopNAndOthersDemo
        Inherits ChartsDemoModule

        Public Sub New()
            InitializeComponent()
        End Sub

        Private Sub AnimateChart(ByVal sender As Object, ByVal e As RoutedEventArgs)
            chart.Animate()
        End Sub
    End Class

    Public Class SelectedIndexToVisibilityConverter
        Implements IValueConverter

        Private Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            Try
                Dim index As Integer = System.Convert.ToInt32(value)
                Dim expectedIndex As Integer = System.Convert.ToInt32(parameter)
                If index = expectedIndex Then Return Visibility.Visible
            Catch
            End Try

            Return Visibility.Collapsed
        End Function

        Private Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Return value
        End Function
    End Class
End Namespace